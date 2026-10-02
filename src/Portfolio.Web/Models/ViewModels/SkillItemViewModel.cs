namespace Portfolio.Web.Models.ViewModels;

/// <summary>
/// A single skill row with its visual proficiency mapping resolved.
/// </summary>
public sealed record SkillItemViewModel(
    string Name,
    string Icon,
    string LevelLabel,
    int LevelPercentage,
    string LevelCssClass);
