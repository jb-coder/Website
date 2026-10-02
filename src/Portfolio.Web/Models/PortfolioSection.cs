namespace Portfolio.Web.Models;

/// <summary>
/// Immutable section descriptor composed with <c>PortfolioSectionBuilder</c>
/// and consumed by <c>SectionHeader</c>.
/// </summary>
public sealed record PortfolioSection(
    string Id,
    string? Eyebrow,
    string Title,
    string? Subtitle,
    string CssClass);
