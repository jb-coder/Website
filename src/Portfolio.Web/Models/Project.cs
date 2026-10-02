namespace Portfolio.Web.Models;

/// <summary>
/// Domain representation of a portfolio project. Text is bilingual; the
/// factory resolves it before it reaches the UI.
/// </summary>
public sealed record Project(
    string Id,
    LocalizedText Title,
    LocalizedText Summary,
    LocalizedText Description,
    ProjectCategory Category,
    ProjectStatus Status,
    int Year,
    string Accent,
    string Icon,
    bool IsFeatured,
    IReadOnlyList<LocalizedText> Highlights,
    IReadOnlyList<Technology> Technologies,
    string? RepositoryUrl = null,
    string? LiveUrl = null);
