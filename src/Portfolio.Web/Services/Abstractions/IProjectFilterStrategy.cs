using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Abstractions;

/// <summary>
/// Strategy Pattern: resolves how the projects page filters its catalogue.
/// A <c>null</c> category means "no filter".
/// </summary>
public interface IProjectFilterStrategy
{
    bool CanHandle(ProjectCategory? category);

    IReadOnlyList<Project> Apply(IReadOnlyList<Project> projects);
}
