using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;

namespace Portfolio.Web.Services.Factories;

/// <summary>
/// Builds <see cref="ProjectCardViewModel"/> instances. Delegates badge creation
/// to the injected <see cref="ITechnologyBadgeStrategy"/> family, so adding a
/// technology category never modifies this class.
/// </summary>
public sealed class ProjectCardFactory(IEnumerable<ITechnologyBadgeStrategy> badgeStrategies) : IProjectCardFactory
{
    public ProjectCardViewModel Create(Project project)
    {
        ArgumentNullException.ThrowIfNull(project);

        var badges = project.Technologies.Select(CreateBadge).ToArray();
        var (statusLabel, statusCssClass) = MapStatus(project.Status);

        return new ProjectCardViewModel(
            project.Id,
            project.Title,
            project.Summary,
            project.Description,
            MapCategory(project.Category),
            statusLabel,
            statusCssClass,
            project.Year,
            project.Accent,
            project.Icon,
            project.IsFeatured,
            project.Highlights,
            badges,
            project.RepositoryUrl,
            project.LiveUrl);
    }

    public IReadOnlyList<ProjectCardViewModel> CreateMany(IEnumerable<Project> projects)
    {
        ArgumentNullException.ThrowIfNull(projects);
        return projects.Select(Create).ToArray();
    }

    private TechnologyBadgeViewModel CreateBadge(Technology technology)
    {
        var strategy = badgeStrategies.FirstOrDefault(candidate => candidate.CanHandle(technology.Category))
            ?? throw new InvalidOperationException(
                $"No badge strategy is registered for category '{technology.Category}'.");

        return strategy.Create(technology);
    }

    private static string MapCategory(ProjectCategory category) => category switch
    {
        ProjectCategory.Api => "APIs & Services",
        ProjectCategory.WebApp => "Web Platform",
        ProjectCategory.Mobile => "Mobile",
        ProjectCategory.Cloud => "Cloud & DevOps",
        _ => category.ToString(),
    };

    private static (string Label, string CssClass) MapStatus(ProjectStatus status) => status switch
    {
        ProjectStatus.InProduction => ("In production", "pf-status--live"),
        ProjectStatus.InProgress => ("In progress", "pf-status--progress"),
        _ => ("Delivered", "pf-status--delivered"),
    };
}
