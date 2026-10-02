namespace Portfolio.Web.Models;

/// <summary>
/// A concrete capability inside a <see cref="TechnologyCategory"/>.
/// </summary>
public sealed record Skill(
    string Name,
    TechnologyCategory Category,
    SkillLevel Level,
    string Icon);
