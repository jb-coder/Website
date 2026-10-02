using Microsoft.AspNetCore.Components;
using Portfolio.Web.Features.Career;
using Portfolio.Web.Features.Telemetry;
using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Localization;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Pages;

/// <summary>
/// Professional profile and career timeline.
/// </summary>
public partial class About : ComponentBase
{
    private string pageTitle = string.Empty;
    private IReadOnlyList<Experience> timeline = [];
    private PortfolioSection timelineSection = default!;

    [Inject]
    private IMediator Mediator { get; set; } = default!;

    [Inject]
    private LocalizedProfile Profile { get; set; } = default!;

    [Inject]
    private IPortfolioSectionBuilder SectionBuilder { get; set; } = default!;

    [Inject]
    private ITranslator Localizer { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        pageTitle = $"{Localizer["Nav.About"]} — {Profile.Name}";

        timelineSection = SectionBuilder
            .WithId("timeline-list")
            .WithEyebrow(Localizer["About.Timeline.Eyebrow"])
            .WithTitle(Localizer["About.Timeline.Title"])
            .WithSubtitle(Localizer["About.Timeline.Subtitle"])
            .Build();

        timeline = await Mediator.SendAsync(new GetCareerTimelineQuery());

        await Mediator.PublishAsync(new PageVisitedNotification("About"));
    }
}
