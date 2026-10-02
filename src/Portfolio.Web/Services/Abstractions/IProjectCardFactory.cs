using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;

namespace Portfolio.Web.Services.Abstractions;

/// <summary>
/// Factory Pattern: centralizes the mapping domain model to card view model so
/// components never contain projection logic.
/// </summary>
public interface IProjectCardFactory
{
    ProjectCardViewModel Create(Project project);

    IReadOnlyList<ProjectCardViewModel> CreateMany(IEnumerable<Project> projects);
}
