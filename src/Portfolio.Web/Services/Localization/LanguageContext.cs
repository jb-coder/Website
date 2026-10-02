using Microsoft.AspNetCore.Components;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Localization;

/// <summary>
/// Resolves the language from the <c>es</c>/<c>en</c> path prefix. Paths are
/// relative to the <c>&lt;base href&gt;</c>, so the same logic works at the
/// site root and under a GitHub Pages repository sub-path.
/// </summary>
public sealed class LanguageContext(NavigationManager navigation) : ILanguageContext
{
    private const string SpanishSegment = "es";
    private const string EnglishSegment = "en";

    public Language Language =>
        HasPrefix(RelativePath, EnglishSegment) ? Language.En : Language.Es;

    public Language OtherLanguage =>
        Language == Language.En ? Language.Es : Language.En;

    public string NeutralPath
    {
        get
        {
            var path = StripLanguage(RelativePath);
            var queryIndex = path.IndexOf('?', StringComparison.Ordinal);
            if (queryIndex >= 0)
            {
                path = path[..queryIndex];
            }

            return path.Trim('/');
        }
    }

    public string BuildHref(string path)
    {
        var normalized = path.TrimStart('/');
        var segment = Language.Code();

        return string.IsNullOrEmpty(normalized)
            ? segment
            : $"{segment}/{normalized}";
    }

    public string SwitchHref()
    {
        var neutral = NeutralPath;
        var segment = OtherLanguage.Code();

        return (string.IsNullOrEmpty(neutral) ? segment : $"{segment}/{neutral}") + QueryString;
    }

    private string RelativePath => navigation.ToBaseRelativePath(navigation.Uri);

    private string QueryString
    {
        get
        {
            var relative = RelativePath;
            var index = relative.IndexOf('?', StringComparison.Ordinal);
            return index >= 0 ? relative[index..] : string.Empty;
        }
    }

    private static bool HasPrefix(string relative, string code) =>
        relative.Equals(code, StringComparison.OrdinalIgnoreCase) ||
        relative.StartsWith(code + "/", StringComparison.OrdinalIgnoreCase) ||
        relative.StartsWith(code + "?", StringComparison.OrdinalIgnoreCase);

    private static string StripLanguage(string relative)
    {
        if (HasPrefix(relative, EnglishSegment))
        {
            return relative[EnglishSegment.Length..].TrimStart('/');
        }

        if (HasPrefix(relative, SpanishSegment))
        {
            return relative[SpanishSegment.Length..].TrimStart('/');
        }

        return relative;
    }
}
