using Portfolio.Web.Services.Localization;
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

    [Inject]
    private ITranslator Localizer { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        pageTitle = $"{Localizer["Skills.Eyebrow"]} — {Localizer["Skills.Title"]}";

        skillsSection = SectionBuilder
            .WithId("skills-matrix")
            .WithEyebrow(Localizer["Skills.Section.Eyebrow"])
            .WithTitle(Localizer["Skills.Section.Title"])
            .WithSubtitle(Localizer["Skills.Section.Subtitle"])
            .Build();

        groups = await Mediator.SendAsync(new GetSkillGroupsQuery());

        await Mediator.PublishAsync(new PageVisitedNotification("Skills"));
    }
}
