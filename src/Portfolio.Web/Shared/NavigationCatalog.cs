using Portfolio.Web.Models;

namespace Portfolio.Web.Shared;

/// <summary>
/// A primary navigation entry. Paths are intentionally relative so they resolve
/// against the <c>&lt;base href&gt;</c> and the language prefix, which makes
/// them work both at the root and under a GitHub Pages sub-path.
/// </summary>
public sealed record NavigationItem(string Path, LocalizedText Label, string Icon);

/// <summary>
/// Single source of truth for the site navigation. The header, the footer and
/// the sitemap consume the same list, so they can never drift apart.
/// </summary>
public static class NavigationCatalog
{
    public static IReadOnlyList<NavigationItem> Items { get; } =
    [
        new NavigationItem("", new LocalizedText("Inicio", "Home"), "home"),
        new NavigationItem("about", new LocalizedText("Sobre mí", "About"), "user"),
        new NavigationItem("skills", new LocalizedText("Habilidades", "Skills"), "grid"),
        new NavigationItem("projects", new LocalizedText("Proyectos", "Projects"), "box"),
        new NavigationItem("architecture", new LocalizedText("Arquitectura", "Architecture"), "layers"),
        new NavigationItem("contact", new LocalizedText("Contacto", "Contact"), "mail"),
    ];
}
