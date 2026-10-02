namespace Portfolio.Web.Models;

/// <summary>
/// Languages supported by the site. Spanish is the default language served at
/// the root; English lives under the <c>/en</c> path prefix.
/// </summary>
public enum Language
{
    Es,
    En,
}

/// <summary>
/// Helpers to convert between <see cref="Language"/> and culture/URL codes.
/// </summary>
public static class LanguageExtensions
{
    public static string Code(this Language language) =>
        language == Language.En ? "en" : "es";

    public static Language FromCode(string? code) =>
        string.Equals(code, "en", StringComparison.OrdinalIgnoreCase)
            ? Language.En
            : Language.Es;
}
