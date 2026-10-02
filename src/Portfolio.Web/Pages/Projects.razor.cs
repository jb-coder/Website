using Microsoft.AspNetCore.Components;
using Portfolio.Web.Features.Projects;
using Portfolio.Web.Features.Telemetry;
using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Localization;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Pages;

/// <summary>
/// Project catalogue. The active filter lives in the query string, so every
/// filtered view keeps a stable URL and the language switcher preserves it.
/// </summary>
public partial class Projects : ComponentBase
{
    private static readonly (string? Value, string Icon)[] FilterDefinitions =
    [
        (null, "grid"),
        (nameof(ProjectCategory.Api), "globe"),
        (nameof(ProjectCategory.WebApp), "layout"),
        (nameof(ProjectCategory.Mobile), "smartphone"),
        (nameof(ProjectCategory.Cloud), "cloud"),
    ];

    private string pageTitle = string.Empty;
    private IReadOnlyList<ProjectCardViewModel> projects = [];
    private PortfolioSection projectsSection = default!;
    private IReadOnlyList<(string? Value, string Label, string Icon)> filters = [];

    [SupplyParameterFromQuery(Name = "category")]
    public string? Category { get; set; }

    [Inject]
    private IMediator Mediator { get; set; } = default!;

    [Inject]
    private ILanguageContext LanguageContext { get; set; } = default!;

    [Inject]
    private IPortfolioSectionBuilder SectionBuilder { get; set; } = default!;

    [Inject]
    private ITranslator Localizer { get; set; } = default!;

    private string CategoryLabel => ParseCategory() switch
    {
        ProjectCategory.Api => Localizer["ProjectCategory.Api"],
        ProjectCategory.WebApp => Localizer["ProjectCategory.WebApp"],
        ProjectCategory.Mobile => Localizer["ProjectCategory.Mobile"],
        ProjectCategory.Cloud => Localizer["ProjectCategory.Cloud"],
        _ => Localizer["Projects.Filter.All"],
    };

    protected override async Task OnInitializedAsync()
    {
        pageTitle = $"{Localizer["Projects.Eyebrow"]} — {Localizer["Projects.Title"]}";

        projectsSection = SectionBuilder
            .WithId("projects-catalogue")
            .WithEyebrow(Localizer["Projects.Section.Eyebrow"])
            .WithTitle(Localizer["Projects.Section.Title"])
            .WithSubtitle(Localizer["Projects.Section.Subtitle"])
            .Build();

        filters = FilterDefinitions
            .Select(definition => (
                definition.Value,
                Localizer[FilterResourceKey(definition.Value)],
                definition.Icon))
            .ToArray();
    }

    protected override async Task OnParametersSetAsync()
    {
        projects = await Mediator.SendAsync(new GetProjectsQuery(ParseCategory()));
        await Mediator.PublishAsync(new PageVisitedNotification("Projects"));
    }

    private bool IsActive(string? value) =>
        string.Equals(Category ?? string.Empty, value ?? string.Empty, StringComparison.OrdinalIgnoreCase);

    private string BuildFilterHref(string? value) =>
        LanguageContext.BuildHref(string.IsNullOrEmpty(value) ? "projects" : $"projects?category={value}");

    private ProjectCategory? ParseCategory() =>
        Enum.TryParse<ProjectCategory>(Category, ignoreCase: true, out var category)
            ? category
            : null;

    private static string FilterResourceKey(string? value) =>
        string.IsNullOrEmpty(value) ? "Projects.Filter.All" : $"Projects.Filter.{value}";
}
