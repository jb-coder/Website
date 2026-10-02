using Microsoft.AspNetCore.Components;
using Portfolio.Web.Features.Skills;
using Portfolio.Web.Features.Telemetry;
using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Pages;

/// <summary>
/// Skills matrix grouped by discipline.
/// </summary>
public partial class Skills : ComponentBase
{
    private string pageTitle = string.Empty;
    private IReadOnlyList<SkillGroupViewModel> groups = [];
    private PortfolioSection skillsSection = default!;

    [Inject]
    private IMediator Mediator { get; set; } = default!;

    [Inject]
    private IPortfolioSectionBuilder SectionBuilder { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        pageTitle = "Skills — .NET, Blazor, Azure and Architecture";

        skillsSection = SectionBuilder
            .WithId("skills-matrix")
            .WithEyebrow("Capabilities")
            .WithTitle("Five disciplines, one coherent toolbox")
            .WithSubtitle("Backend, frontend, mobile, cloud and architecture — each with real delivery behind it.")
            .Build();

        groups = await Mediator.SendAsync(new GetSkillGroupsQuery());

        await Mediator.PublishAsync(new PageVisitedNotification("Skills"));
    }
}
