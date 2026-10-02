using System.Xml.Linq;

// Static exporter: snapshots every route of a running Blazor Web App
// (static SSR) into plain HTML files that GitHub Pages can host.
//
// Usage:
//   dotnet run --project tools/Portfolio.StaticExporter -- \
//     --source http://localhost:5199 \
//     --output ./dist \
//     --static-dir src/Portfolio.Web/bin/Release/net10.0/publish/wwwroot \
//     --base-path /my-repo/ \
//     [--public-url https://user.github.io]

const string SitemapNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

var arguments = ParseArguments(args);
var source = GetRequired(arguments, "--source").TrimEnd('/');
var output = Path.GetFullPath(GetRequired(arguments, "--output"));
var staticDirectory = Path.GetFullPath(GetRequired(arguments, "--static-dir"));
var basePath = NormalizeBasePath(arguments.GetValueOrDefault("--base-path") ?? "/");
var publicUrl = arguments.GetValueOrDefault("--public-url")?.TrimEnd('/');

if (!Directory.Exists(staticDirectory))
{
    Console.Error.WriteLine($"Static assets directory not found: {staticDirectory}");
    return 1;
}

using var http = new HttpClient
{
    BaseAddress = new Uri($"{source}{basePath}"),
    Timeout = TimeSpan.FromSeconds(60),
};

try
{
    Console.WriteLine($"Exporting {source}{basePath} -> {output}");

    var sitemapContent = await http.GetStringAsync("sitemap.xml");
    var routes = ReadRoutes(sitemapContent, basePath);
    Console.WriteLine($"Sitemap routes discovered: {routes.Count}");

    if (Directory.Exists(output))
    {
        Directory.Delete(output, recursive: true);
    }

    CopyDirectory(staticDirectory, output);

    foreach (var route in routes)
    {
        var html = await http.GetStringAsync(route);
        var target = ResolveTarget(output, route);
        Directory.CreateDirectory(Path.GetDirectoryName(target)!);
        await File.WriteAllTextAsync(target, html);
        Console.WriteLine($"  rendered /{route} -> {Path.GetRelativePath(output, target)}");
    }

    using (var notFoundResponse = await http.GetAsync("this-page-does-not-exist"))
    {
        var notFoundHtml = await notFoundResponse.Content.ReadAsStringAsync();
        await File.WriteAllTextAsync(Path.Combine(output, "404.html"), notFoundHtml);
    }
    Console.WriteLine("  rendered 404.html");

    await File.WriteAllTextAsync(
        Path.Combine(output, "sitemap.xml"),
        BuildSitemap(routes, basePath, publicUrl) ?? sitemapContent);

    await File.WriteAllTextAsync(
        Path.Combine(output, "robots.txt"),
        BuildRobots(basePath, publicUrl) ?? await http.GetStringAsync("robots.txt"));

    await File.WriteAllTextAsync(Path.Combine(output, ".nojekyll"), string.Empty);

    Console.WriteLine($"Export completed: {routes.Count + 1} HTML files.");
    return 0;
}
catch (Exception exception)
{
    Console.Error.WriteLine($"Export failed: {exception.Message}");
    return 1;
}

static Dictionary<string, string> ParseArguments(string[] args)
{
    var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    for (var index = 0; index < args.Length; index++)
    {
        var argument = args[index];
        if (!argument.StartsWith("--", StringComparison.Ordinal))
        {
            continue;
        }

        var separator = argument.IndexOf('=');
        if (separator > 0)
        {
            result[argument[..separator]] = argument[(separator + 1)..];
            continue;
        }

        if (index + 1 < args.Length && !args[index + 1].StartsWith("--", StringComparison.Ordinal))
        {
            result[argument] = args[++index];
        }
        else
        {
            result[argument] = string.Empty;
        }
    }

    return result;
}

static string GetRequired(Dictionary<string, string> arguments, string name) =>
    arguments.TryGetValue(name, out var value) && !string.IsNullOrWhiteSpace(value)
        ? value
        : throw new ArgumentException($"Missing required argument '{name}'.");

static string NormalizeBasePath(string basePath)
{
    if (string.IsNullOrWhiteSpace(basePath) || basePath == "/")
    {
        return "/";
    }

    var trimmed = basePath.Trim();
    if (!trimmed.StartsWith('/'))
    {
        trimmed = "/" + trimmed;
    }

    return trimmed.EndsWith('/') ? trimmed : trimmed + "/";
}

static List<string> ReadRoutes(string sitemapContent, string basePath)
{
    XNamespace ns = SitemapNamespace;

    return XDocument.Parse(sitemapContent)
        .Descendants(ns + "loc")
        .Select(element => new Uri(element.Value).AbsolutePath)
        .Select(path => ToRoute(path, basePath))
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .ToList();
}

static string ToRoute(string absolutePath, string basePath)
{
    var trimmedBase = basePath.TrimEnd('/');

    if (absolutePath.Equals(trimmedBase, StringComparison.OrdinalIgnoreCase))
    {
        return string.Empty;
    }

    if (absolutePath.StartsWith(basePath, StringComparison.OrdinalIgnoreCase))
    {
        return absolutePath[basePath.Length..].Trim('/');
    }

    return absolutePath.Trim('/');
}

static string ResolveTarget(string output, string route)
{
    var relative = route.Length == 0
        ? "index.html"
        : Path.Combine(route.Split('/').ToArray()) + Path.DirectorySeparatorChar + "index.html";

    return Path.Combine(output, relative);
}

static void CopyDirectory(string source, string destination)
{
    Directory.CreateDirectory(destination);

    foreach (var file in Directory.GetFiles(source))
    {
        // Pre-compressed variants are unnecessary on GitHub Pages and would
        // double the artifact size; the site never references Blazor framework
        // scripts, so they are skipped too.
        if (file.EndsWith(".br", StringComparison.OrdinalIgnoreCase) ||
            file.EndsWith(".gz", StringComparison.OrdinalIgnoreCase))
        {
            continue;
        }

        File.Copy(file, Path.Combine(destination, Path.GetFileName(file)), overwrite: true);
    }

    foreach (var directory in Directory.GetDirectories(source))
    {
        var name = Path.GetFileName(directory);
        if (name.Equals("_framework", StringComparison.OrdinalIgnoreCase))
        {
            continue;
        }

        CopyDirectory(directory, Path.Combine(destination, name));
    }
}

static string? BuildSitemap(List<string> routes, string basePath, string? publicUrl)
{
    if (publicUrl is null)
    {
        return null;
    }

    XNamespace ns = SitemapNamespace;
    var root = $"{publicUrl}{basePath}";

    var document = new XDocument(
        new XDeclaration("1.0", "utf-8", null),
        new XElement(
            ns + "urlset",
            routes.Select(route => new XElement(
                ns + "url",
                new XElement(ns + "loc", route.Length == 0 ? root.TrimEnd('/') : root + route)))));

    return document.ToString();
}

static string? BuildRobots(string basePath, string? publicUrl)
{
    if (publicUrl is null)
    {
        return null;
    }

    return $"User-agent: *{Environment.NewLine}Allow: /{Environment.NewLine}{Environment.NewLine}Sitemap: {publicUrl}{basePath}sitemap.xml{Environment.NewLine}";
}
