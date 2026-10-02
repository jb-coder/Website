using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Portfolio.Web.Features.Career;
using Portfolio.Web.Features.Telemetry;
using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Mediator;
using Portfolio.Web.Services.Options;

namespace Portfolio.Web.Pages;

/// <summary>
/// Professional profile and career timeline.
/// </summary>
public partial class About : ComponentBase
{
    private PortfolioOptions portfolio = default!;
    private string pageTitle = string.Empty;
    private IReadOnlyList<Experience> timeline = [];
    private PortfolioSection timelineSection = default!;

    [Inject]
    private IMediator Mediator { get; set; } = default!;

    [Inject]
    private IOptions<PortfolioOptions> PortfolioOptions { get; set; } = default!;

    [Inject]
    private IPortfolioSectionBuilder SectionBuilder { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        portfolio = PortfolioOptions.Value;
        pageTitle = $"About — {portfolio.Name}";

        timelineSection = SectionBuilder
            .WithId("timeline-list")
            .WithEyebrow("Career")
            .WithTitle("Experience and milestones")
            .WithSubtitle("From electronics repair to leading development teams: the path that shaped how I build software.")
            .Build();

        timeline = await Mediator.SendAsync(new GetCareerTimelineQuery());

        await Mediator.PublishAsync(new PageVisitedNotification("About"));
    }
}
