using Microsoft.AspNetCore.Components;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Localization;

/// <summary>
/// Resolves the language from the <c>en</c> path prefix. Paths are relative to
/// the <c>&lt;base href&gt;</c>, so the same logic works at the site root and
/// under a GitHub Pages repository sub-path.
/// </summary>
public sealed class LanguageContext(NavigationManager navigation) : ILanguageContext
{
    private const string EnglishSegment = "en";

    public Language Language =>
        HasEnglishPrefix(RelativePath) ? Language.En : Language.Es;

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

        if (Language == Language.Es)
        {
            return normalized;
        }

        return string.IsNullOrEmpty(normalized)
            ? EnglishSegment
            : $"{EnglishSegment}/{normalized}";
    }

    public string SwitchHref()
    {
        var neutral = NeutralPath;

        if (OtherLanguage == Language.Es)
        {
            return neutral + QueryString;
        }

        return (string.IsNullOrEmpty(neutral) ? EnglishSegment : $"{EnglishSegment}/{neutral}") + QueryString;
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

    private static bool HasEnglishPrefix(string relative) =>
        relative.Equals(EnglishSegment, StringComparison.OrdinalIgnoreCase) ||
        relative.StartsWith(EnglishSegment + "/", StringComparison.OrdinalIgnoreCase) ||
        relative.StartsWith(EnglishSegment + "?", StringComparison.OrdinalIgnoreCase);

    private static string StripLanguage(string relative) =>
        HasEnglishPrefix(relative)
            ? relative[EnglishSegment.Length..].TrimStart('/')
            : relative;
}
