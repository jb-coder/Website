using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;

namespace Portfolio.Web.Services.Strategies;

/// <summary>
/// Base for the per-category filters. Each concrete filter only declares the
/// category it owns.
/// </summary>
public abstract class ProjectCategoryFilterStrategyBase : IProjectFilterStrategy
{
    protected abstract ProjectCategory Category { get; }

    public bool CanHandle(ProjectCategory? category) => category == Category;

    public IReadOnlyList<Project> Apply(IReadOnlyList<Project> projects)
    {
        ArgumentNullException.ThrowIfNull(projects);
        return projects.Where(project => project.Category == Category).ToArray();
    }
}
