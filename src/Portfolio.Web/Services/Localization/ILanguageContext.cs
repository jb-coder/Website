using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Localization;

/// <summary>
/// Per-request language resolved from the URL. It powers language-aware links
/// and the language switcher without any client-side state.
/// </summary>
public interface ILanguageContext
{
    /// <summary>Language of the page being rendered.</summary>
    Language Language { get; }

    /// <summary>The other supported language.</summary>
    Language OtherLanguage { get; }

    /// <summary>
    /// Current page path without the <c>es</c>/<c>en</c> prefix and without the
    /// query string, e.g. <c>projects</c>. Empty for the home page.
    /// </summary>
    string NeutralPath { get; }

    /// <summary>
    /// Builds a site-relative href for the current language, preserving the
    /// base path resolution of the document.
    /// </summary>
    string BuildHref(string path);

    /// <summary>
    /// Href of the current page in the other language, preserving the query
    /// string (used by filters).
    /// </summary>
    string SwitchHref();
}
