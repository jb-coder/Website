namespace Portfolio.Web.Models.ViewModels;

/// <summary>
/// Presentation-ready technology badge. Produced by the
/// <c>ITechnologyBadgeStrategy</c> family, never by the UI itself.
/// </summary>
public sealed record TechnologyBadgeViewModel(
    string Name,
    string CategoryLabel,
    string CssClass,
    string Icon,
    string Accent,
    string AriaLabel);
