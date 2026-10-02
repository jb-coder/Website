using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Portfolio.Web.Features.Home;
using Portfolio.Web.Features.Projects;
using Portfolio.Web.Features.Telemetry;
using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Localization;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Pages;

/// <summary>
/// Home orchestrator. Every piece of data is requested through the mediator;
/// the page never talks to a repository or factory directly.
/// </summary>
public partial class Home : ComponentBase
{
    private LocalizedProfile profile = default!;
    private string pageTitle = string.Empty;
    private string structuredData = string.Empty;
    private IReadOnlyList<StatsCardViewModel> statistics = [];
    private IReadOnlyList<ProjectCardViewModel> featuredProjects = [];
    private IReadOnlyList<TechnologyBadgeViewModel> technologies = [];
    private PortfolioSection featuredSection = default!;
    private PortfolioSection principlesSection = default!;
    private IReadOnlyList<(string Icon, string Title, string Text)> principles = [];

    [Inject]
    private IMediator Mediator { get; set; } = default!;

    [Inject]
    private LocalizedProfile Profile { get; set; } = default!;

    [Inject]
    private ILanguageContext LanguageContext { get; set; } = default!;

    [Inject]
    private IPortfolioSectionBuilder SectionBuilder { get; set; } = default!;

    [Inject]
    private ITranslator Localizer { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        profile = Profile;
        pageTitle = $"{profile.Name} — {profile.Role}";
        structuredData = BuildStructuredData();

        featuredSection = SectionBuilder
            .WithId("featured-work")
            .WithEyebrow(Localizer["Home.Featured.Eyebrow"])
            .WithTitle(Localizer["Home.Featured.Title"])
            .WithSubtitle(Localizer["Home.Featured.Subtitle"])
            .Build();

        principlesSection = SectionBuilder
            .WithId("how-i-work")
            .WithEyebrow(Localizer["Home.Principles.Eyebrow"])
            .WithTitle(Localizer["Home.Principles.Title"])
            .WithSubtitle(Localizer["Home.Principles.Subtitle"])
            .Build();

        principles =
        [
            (
                "layers",
                Localizer["Home.Principle.Architecture.Title"],
                Localizer["Home.Principle.Architecture.Text"]),
            (
                "refresh-cw",
                Localizer["Home.Principle.Automation.Title"],
                Localizer["Home.Principle.Automation.Text"]),
            (
                "zap",
                Localizer["Home.Principle.Performance.Title"],
                Localizer["Home.Principle.Performance.Text"]),
        ];

        statistics = await Mediator.SendAsync(new GetPortfolioStatisticsQuery());
        featuredProjects = await Mediator.SendAsync(new GetFeaturedProjectsQuery(3));
        technologies = await Mediator.SendAsync(new GetCoreTechnologiesQuery());

        await Mediator.PublishAsync(new PageVisitedNotification("Home"));
    }

    private string BuildStructuredData()
    {
        var person = new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = "Person",
            ["name"] = profile.Name,
            ["jobTitle"] = profile.Role,
            ["description"] = profile.Tagline,
            ["email"] = $"mailto:{profile.Email}",
            ["url"] = profile.PublicBaseUrl,
            ["inLanguage"] = LanguageContext.Language.Code(),
            ["sameAs"] = new[] { profile.LinkedInUrl, profile.GitHubUrl },
            ["address"] = new Dictionary<string, object?>
            {
                ["@type"] = "PostalAddress",
                ["addressLocality"] = profile.Location,
            },
            ["knowsAbout"] = profile.FocusAreas,
        };

        return JsonSerializer.Serialize(person);
    }
}
