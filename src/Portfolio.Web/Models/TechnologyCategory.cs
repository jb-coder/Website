namespace Portfolio.Web.Models;

/// <summary>
/// Knowledge domain of a technology or skill. Drives the Strategy Pattern used
/// to render technology badges and to group the skills matrix.
/// </summary>
public enum TechnologyCategory
{
    Backend,
    Frontend,
    Mobile,
    Cloud,
    Architecture,
}
