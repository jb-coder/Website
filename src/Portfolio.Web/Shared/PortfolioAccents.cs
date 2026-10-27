using Portfolio.Web.Models;

namespace Portfolio.Web.Shared;

/// <summary>
/// Semantic accent tokens shared by UI-facing services. The values are CSS
/// custom properties, so the palette remains defined in
/// <c>wwwroot/Themes/theme.css</c> and never drifts across C# files.
/// </summary>
public static class PortfolioAccents
{
    public const string Primary = "var(--pf-accent-primary)";
    public const string Secondary = "var(--pf-accent-secondary)";
    public const string Backend = "var(--pf-accent-backend)";
    public const string Frontend = "var(--pf-accent-frontend)";
    public const string Mobile = "var(--pf-accent-mobile)";
    public const string Cloud = "var(--pf-accent-cloud)";
    public const string Architecture = "var(--pf-accent-architecture)";

    /// <summary>Returns the accent token associated with a technology category.</summary>
    public static string For(TechnologyCategory category) => category switch
    {
        TechnologyCategory.Backend => Backend,
        TechnologyCategory.Frontend => Frontend,
        TechnologyCategory.Mobile => Mobile,
        TechnologyCategory.Cloud => Cloud,
        _ => Architecture,
    };
}
