namespace Portfolio.Web.Models.ViewModels;

/// <summary>
/// Skills grouped by domain, ready for the skills matrix.
/// </summary>
public sealed record SkillGroupViewModel(
    string Id,
    string Title,
    string Description,
    string Icon,
    string Accent,
    IReadOnlyList<SkillItemViewModel> Skills);
