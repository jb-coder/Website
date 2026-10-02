using Microsoft.AspNetCore.Components;
using Portfolio.Web.Features.Telemetry;
using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Localization;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Pages;

/// <summary>
/// Contact channels. Every link is parameterizable through PortfolioOptions.
/// </summary>
public partial class Contact : ComponentBase
{
    private string pageTitle = string.Empty;
    private PortfolioSection contactSection = default!;

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
        pageTitle = $"{Localizer["Contact.Eyebrow"]} — {Profile.Name}";

        contactSection = SectionBuilder
            .WithId("contact-channels")
            .WithEyebrow(Localizer["Contact.Section.Eyebrow"])
            .WithTitle(Localizer["Contact.Section.Title"])
            .WithSubtitle(Localizer["Contact.Section.Subtitle"])
            .Build();

        await Mediator.PublishAsync(new PageVisitedNotification("Contact"));
    }

    private string BuildMapUrl() =>
        $"https://www.google.com/maps/search/?api=1&query={Uri.EscapeDataString(Profile.Location)}";
}
