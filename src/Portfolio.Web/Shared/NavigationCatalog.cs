namespace Portfolio.Web.Shared;

/// <summary>
/// A primary navigation entry. Paths are intentionally relative so they resolve
/// against the <c>&lt;base href&gt;</c>, which makes them work both at the root
/// and under a GitHub Pages sub-path.
/// </summary>
public sealed record NavigationItem(string Path, string Label, string Icon);

/// <summary>
/// Single source of truth for the site navigation. The sitemap endpoint and the
/// header consume the same list, so they can never drift apart.
/// </summary>
public static class NavigationCatalog
{
    public static IReadOnlyList<NavigationItem> Items { get; } =
    [
        new NavigationItem("", "Home", "home"),
        new NavigationItem("about", "About", "user"),
        new NavigationItem("skills", "Skills", "grid"),
        new NavigationItem("projects", "Projects", "box"),
        new NavigationItem("architecture", "Architecture", "layers"),
        new NavigationItem("contact", "Contact", "mail"),
    ];
}
