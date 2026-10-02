using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Portfolio.Web.Features.Telemetry;
using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Mediator;
using Portfolio.Web.Services.Options;

namespace Portfolio.Web.Pages;

/// <summary>
/// Contact channels. Every link is parameterizable through PortfolioOptions.
/// </summary>
public partial class Contact : ComponentBase
{
    private PortfolioOptions portfolio = default!;
    private string pageTitle = string.Empty;
    private PortfolioSection contactSection = default!;

    [Inject]
    private IMediator Mediator { get; set; } = default!;

    [Inject]
    private IOptions<PortfolioOptions> PortfolioOptions { get; set; } = default!;

    [Inject]
    private IPortfolioSectionBuilder SectionBuilder { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        portfolio = PortfolioOptions.Value;
        pageTitle = $"Contact — {portfolio.Name}";

        contactSection = SectionBuilder
            .WithId("contact-channels")
            .WithEyebrow("Channels")
            .WithTitle("Pick the channel that suits you")
            .WithSubtitle("All links are driven by configuration, so they can never go stale in the markup.")
            .Build();

        await Mediator.PublishAsync(new PageVisitedNotification("Contact"));
    }

    private string BuildMapUrl() =>
        $"https://www.google.com/maps/search/?api=1&query={Uri.EscapeDataString(portfolio.Location)}";
}
