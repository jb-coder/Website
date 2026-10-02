namespace Portfolio.Web.Models.ViewModels;

/// <summary>
/// Everything <c>ProjectCard</c> needs to render. Built by
/// <c>ProjectCardFactory</c> so components stay free of mapping logic.
/// </summary>
public sealed record ProjectCardViewModel(
    string Id,
    string Title,
    string Summary,
    string Description,
    string CategoryLabel,
    string StatusLabel,
    string StatusCssClass,
    int Year,
    string Accent,
    string Icon,
    bool IsFeatured,
    IReadOnlyList<string> Highlights,
    IReadOnlyList<TechnologyBadgeViewModel> Badges,
    string? RepositoryUrl,
    string? LiveUrl);
