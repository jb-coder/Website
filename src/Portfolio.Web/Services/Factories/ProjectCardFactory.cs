using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Localization;

namespace Portfolio.Web.Services.Factories;

/// <summary>
/// Builds <see cref="ProjectCardViewModel"/> instances. Resolves bilingual
/// domain text for the current request and delegates badge creation to the
/// injected <see cref="ITechnologyBadgeStrategy"/> family, so adding a
/// technology category never modifies this class.
/// </summary>
public sealed class ProjectCardFactory(
    IEnumerable<ITechnologyBadgeStrategy> badgeStrategies,
    ILanguageContext language,
    ITranslator localizer) : IProjectCardFactory
{
    public ProjectCardViewModel Create(Project project)
    {
        ArgumentNullException.ThrowIfNull(project);

        var current = language.Language;
        var badges = project.Technologies.Select(CreateBadge).ToArray();
        var (statusLabel, statusCssClass) = MapStatus(project.Status);

        return new ProjectCardViewModel(
            project.Id,
            project.Title.For(current),
            project.Summary.For(current),
            project.Description.For(current),
            localizer[$"ProjectCategory.{project.Category}"],
            statusLabel,
            statusCssClass,
            project.Year,
            project.Accent,
            project.Icon,
            project.IsFeatured,
            project.Highlights.Select(highlight => highlight.For(current)).ToArray(),
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

    private (string Label, string CssClass) MapStatus(ProjectStatus status) => status switch
    {
        ProjectStatus.InProduction => (localizer["ProjectStatus.InProduction"], "pf-status--live"),
        ProjectStatus.InProgress => (localizer["ProjectStatus.InProgress"], "pf-status--progress"),
        _ => (localizer["ProjectStatus.Delivered"], "pf-status--delivered"),
    };
}
