using Microsoft.AspNetCore.Components;
using Portfolio.Web.Features.Projects;
using Portfolio.Web.Features.Telemetry;
using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Pages;

/// <summary>
/// Project catalogue. The active filter lives in the query string, so every
/// filtered view is a real, shareable and pre-renderable URL.
/// </summary>
public partial class Projects : ComponentBase
{
    private static readonly (string? Value, string Label, string Icon)[] Filters =
    [
        (null, "All", "grid"),
        (nameof(ProjectCategory.Api), "APIs & Services", "globe"),
        (nameof(ProjectCategory.WebApp), "Web Platforms", "layout"),
        (nameof(ProjectCategory.Mobile), "Mobile", "smartphone"),
        (nameof(ProjectCategory.Cloud), "Cloud & DevOps", "cloud"),
    ];

    private string pageTitle = string.Empty;
    private IReadOnlyList<ProjectCardViewModel> projects = [];
    private PortfolioSection projectsSection = default!;

    [SupplyParameterFromQuery(Name = "category")]
    public string? Category { get; set; }

    [Inject]
    private IMediator Mediator { get; set; } = default!;

    [Inject]
    private IPortfolioSectionBuilder SectionBuilder { get; set; } = default!;

    private IReadOnlyList<(string? Value, string Label, string Icon)> filters = Filters;

    private string CategoryLabel => ParseCategory()?.ToString() switch
    {
        nameof(ProjectCategory.Api) => "APIs & Services",
        nameof(ProjectCategory.WebApp) => "Web Platforms",
        nameof(ProjectCategory.Mobile) => "Mobile",
        nameof(ProjectCategory.Cloud) => "Cloud & DevOps",
        _ => "all categories",
    };

    protected override async Task OnInitializedAsync()
    {
        pageTitle = "Projects — .NET platforms, APIs and mobile apps";

        projectsSection = SectionBuilder
            .WithId("projects-catalogue")
            .WithEyebrow("Portfolio")
            .WithTitle("Selected projects")
            .WithSubtitle("Each card summarizes the problem, the solution and the outcome.")
            .Build();
    }

    protected override async Task OnParametersSetAsync()
    {
        projects = await Mediator.SendAsync(new GetProjectsQuery(ParseCategory()));
        await Mediator.PublishAsync(new PageVisitedNotification("Projects"));
    }

    private bool IsActive(string? value) =>
        string.Equals(Category ?? string.Empty, value ?? string.Empty, StringComparison.OrdinalIgnoreCase);

    private static string BuildFilterHref(string? value) =>
        string.IsNullOrEmpty(value) ? "projects" : $"projects?category={value}";

    private ProjectCategory? ParseCategory() =>
        Enum.TryParse<ProjectCategory>(Category, ignoreCase: true, out var category)
            ? category
            : null;
}
