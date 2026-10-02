namespace Portfolio.Web.Models;

/// <summary>
/// A technology the author works with. Immutable value object; the
/// <see cref="Category"/> is consumed by the badge strategy family.
/// </summary>
public sealed record Technology(
    string Name,
    TechnologyCategory Category,
    string Icon,
    string Accent);
