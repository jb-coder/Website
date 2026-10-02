namespace Portfolio.Web.Models.ViewModels;

/// <summary>
/// A headline metric shown on the home page.
/// </summary>
public sealed record StatsCardViewModel(
    string Value,
    string Label,
    string Description,
    string Icon,
    string Accent);
