using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;

namespace Portfolio.Web.Services.Strategies;

/// <summary>Default filter: returns the full catalogue.</summary>
public sealed class AllProjectsFilterStrategy : IProjectFilterStrategy
{
    public bool CanHandle(ProjectCategory? category) => category is null;

    public IReadOnlyList<Project> Apply(IReadOnlyList<Project> projects)
    {
        ArgumentNullException.ThrowIfNull(projects);
        return projects;
    }
}
