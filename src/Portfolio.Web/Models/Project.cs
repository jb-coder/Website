namespace Portfolio.Web.Models;

/// <summary>
/// Domain representation of a portfolio project. Data comes from
/// <c>IProjectRepository</c>; presentation concerns live in view models.
/// </summary>
public sealed record Project(
    string Id,
    string Title,
    string Summary,
    string Description,
    ProjectCategory Category,
    ProjectStatus Status,
    int Year,
    string Accent,
    string Icon,
    bool IsFeatured,
    IReadOnlyList<string> Highlights,
    IReadOnlyList<Technology> Technologies,
    string? RepositoryUrl = null,
    string? LiveUrl = null);
