using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Portfolio.Web.Features.Home;
using Portfolio.Web.Features.Projects;
using Portfolio.Web.Features.Telemetry;
using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Mediator;
using Portfolio.Web.Services.Options;

namespace Portfolio.Web.Pages;

/// <summary>
/// Home orchestrator. Every piece of data is requested through the mediator;
/// the page never talks to a repository or factory directly.
/// </summary>
public partial class Home : ComponentBase
{
    private static readonly (string Icon, string Title, string Text)[] Principles =
    [
        (
            "layers",
            "Architecture first",
            "I design boundaries before writing code: clean layering, explicit contracts and a domain that is easy to test."),
        (
            "refresh-cw",
            "Automate everything",
            "Pipelines build, test and deploy every change. If a step is manual, it will eventually be forgotten."),
        (
            "zap",
            "Performance is a feature",
            "Fast APIs and pre-rendered UIs. This site ships zero JavaScript on purpose."),
    ];

    private PortfolioOptions portfolio = default!;
    private string pageTitle = string.Empty;
    private string structuredData = string.Empty;
    private IReadOnlyList<StatsCardViewModel> statistics = [];
    private IReadOnlyList<ProjectCardViewModel> featuredProjects = [];
    private IReadOnlyList<TechnologyBadgeViewModel> technologies = [];
    private PortfolioSection featuredSection = default!;
    private PortfolioSection principlesSection = default!;

    private IReadOnlyList<(string Icon, string Title, string Text)> principles = Principles;

    [Inject]
    private IMediator Mediator { get; set; } = default!;

    [Inject]
    private IOptions<PortfolioOptions> PortfolioOptions { get; set; } = default!;

    [Inject]
    private IPortfolioSectionBuilder SectionBuilder { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        portfolio = PortfolioOptions.Value;
        pageTitle = $"{portfolio.Name} — {portfolio.Role}";
        structuredData = BuildStructuredData();

        featuredSection = SectionBuilder
            .WithId("featured-work")
            .WithEyebrow("Selected work")
            .WithTitle("Projects that shipped and stayed in production")
            .WithSubtitle("A sample of the platforms, APIs and apps I have designed and delivered.")
            .Build();

        principlesSection = SectionBuilder
            .WithId("how-i-work")
            .WithEyebrow("How I work")
            .WithTitle("Engineering principles I do not negotiate")
            .WithSubtitle("The habits behind every repository, pipeline and pull request.")
            .Build();

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
            ["name"] = portfolio.Name,
            ["jobTitle"] = portfolio.Role,
            ["description"] = portfolio.Tagline,
            ["email"] = $"mailto:{portfolio.Email}",
            ["url"] = portfolio.PublicBaseUrl,
            ["sameAs"] = new[] { portfolio.LinkedInUrl, portfolio.GitHubUrl },
            ["address"] = new Dictionary<string, object?>
            {
                ["@type"] = "PostalAddress",
                ["addressLocality"] = portfolio.Location,
            },
            ["knowsAbout"] = portfolio.FocusAreas,
        };

        return JsonSerializer.Serialize(person);
    }
}
