using System.Text;
using System.Xml.Linq;
using Microsoft.Extensions.Options;
using Portfolio.Web.Services.Options;
using Portfolio.Web.Shared;

namespace Portfolio.Web.Services;

/// <summary>
/// Technical endpoints that exist for crawlers and for the static exporter.
/// </summary>
public static class PortfolioEndpoints
{
    private static readonly XNamespace SitemapNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

    public static IEndpointRouteBuilder MapPortfolioEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        endpoints.MapGet("/sitemap.xml", (IOptions<PortfolioOptions> options) =>
            Results.Text(BuildSitemap(options.Value), "application/xml", Encoding.UTF8));

        endpoints.MapGet("/robots.txt", (IOptions<PortfolioOptions> options) =>
            Results.Text(BuildRobots(options.Value), "text/plain", Encoding.UTF8));

        return endpoints;
    }

    private static string BuildSitemap(PortfolioOptions options)
    {
        var root = BuildPublicRoot(options);

        var document = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            new XElement(
                SitemapNamespace + "urlset",
                NavigationCatalog.Items.Select(item =>
                    new XElement(
                        SitemapNamespace + "url",
                        new XElement(SitemapNamespace + "loc", BuildPageUrl(root, item.Path))))));

        return document.ToString();
    }

    private static string BuildRobots(PortfolioOptions options)
    {
        var root = BuildPublicRoot(options);
        return $"User-agent: *{Environment.NewLine}Allow: /{Environment.NewLine}{Environment.NewLine}Sitemap: {root}sitemap.xml{Environment.NewLine}";
    }

    private static string BuildPublicRoot(PortfolioOptions options)
    {
        var publicBase = options.PublicBaseUrl.TrimEnd('/');
        return publicBase + options.NormalizedBasePath;
    }

    private static string BuildPageUrl(string root, string path) =>
        string.IsNullOrEmpty(path) ? root.TrimEnd('/') : root + path;
}
