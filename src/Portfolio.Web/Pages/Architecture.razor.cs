using Portfolio.Web.Services.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Portfolio.Web.Features.Architecture;
using Portfolio.Web.Features.Telemetry;
using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Mediator;
using Portfolio.Web.Services.Options;

namespace Portfolio.Web.Pages;

/// <summary>
/// Architecture page: layers, diagrams, applied patterns, SOLID and quality
/// gates. Pattern cards link to the documentation stored in the repository.
/// </summary>
public partial class Architecture : ComponentBase
{
    private string pageTitle = string.Empty;
    private IReadOnlyList<ArchitecturePattern> patterns = [];
    private PortfolioSection layersSection = default!;
    private PortfolioSection diagramSection = default!;
    private PortfolioSection patternsSection = default!;
    private PortfolioSection solidSection = default!;
    private PortfolioSection qualitySection = default!;
    private IReadOnlyList<(string Icon, string Title, string Text, string[] Examples)> layers = [];
    private IReadOnlyList<(string Letter, string Name, string Text)> solidPrinciples = [];
    private IReadOnlyList<string> qualityGates = [];

    [Inject]
    private IMediator Mediator { get; set; } = default!;

    [Inject]
    private IOptions<PortfolioOptions> PortfolioOptions { get; set; } = default!;

    [Inject]
    private IPortfolioSectionBuilder SectionBuilder { get; set; } = default!;

    [Inject]
    private ITranslator Localizer { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        pageTitle = $"{Localizer["Architecture.Eyebrow"]} — {Localizer["Architecture.Title"]}";

        layersSection = SectionBuilder
            .WithId("request-flow")
            .WithEyebrow(Localizer["Architecture.Layers.Eyebrow"])
            .WithTitle(Localizer["Architecture.Layers.Title"])
            .WithSubtitle(Localizer["Architecture.Layers.Subtitle"])
            .Build();

        diagramSection = SectionBuilder
            .WithId("dependency-diagram")
            .WithEyebrow(Localizer["Architecture.Diagram.Eyebrow"])
            .WithTitle(Localizer["Architecture.Diagram.Title"])
            .WithSubtitle(Localizer["Architecture.Diagram.Subtitle"])
            .Build();

        patternsSection = SectionBuilder
            .WithId("patterns")
            .WithEyebrow(Localizer["Architecture.Patterns.Eyebrow"])
            .WithTitle(Localizer["Architecture.Patterns.Title"])
            .WithSubtitle(Localizer["Architecture.Patterns.Subtitle"])
            .Build();

        solidSection = SectionBuilder
            .WithId("solid")
            .WithEyebrow(Localizer["Architecture.Solid.Eyebrow"])
            .WithTitle(Localizer["Architecture.Solid.Title"])
            .WithSubtitle(Localizer["Architecture.Solid.Subtitle"])
            .Build();

        qualitySection = SectionBuilder
            .WithId("quality")
            .WithEyebrow(Localizer["Architecture.Quality.Eyebrow"])
            .WithTitle(Localizer["Architecture.Quality.Title"])
            .WithSubtitle(Localizer["Architecture.Quality.Subtitle"])
            .Build();

        layers =
        [
            (
                "layout",
                Localizer["Architecture.Layer.Presentation.Title"],
                Localizer["Architecture.Layer.Presentation.Text"],
                ["Layouts", "Pages", "Shared components"]),
            (
                "send",
                Localizer["Architecture.Layer.Application.Title"],
                Localizer["Architecture.Layer.Application.Text"],
                ["IMediator", "Queries", "Handlers"]),
            (
                "grid",
                Localizer["Architecture.Layer.Domain.Title"],
                Localizer["Architecture.Layer.Domain.Text"],
                ["Strategies", "ProjectCardFactory", "SectionBuilder"]),
            (
                "database",
                Localizer["Architecture.Layer.Infrastructure.Title"],
                Localizer["Architecture.Layer.Infrastructure.Text"],
                ["Repositories", "Options", "Configuration"]),
        ];

        solidPrinciples =
        [
            ("S", Localizer["Architecture.Solid.S.Name"], Localizer["Architecture.Solid.S.Text"]),
            ("O", Localizer["Architecture.Solid.O.Name"], Localizer["Architecture.Solid.O.Text"]),
            ("L", Localizer["Architecture.Solid.L.Name"], Localizer["Architecture.Solid.L.Text"]),
            ("I", Localizer["Architecture.Solid.I.Name"], Localizer["Architecture.Solid.I.Text"]),
            ("D", Localizer["Architecture.Solid.D.Name"], Localizer["Architecture.Solid.D.Text"]),
        ];

        qualityGates =
        [
            Localizer["Architecture.Quality.Nullable"],
            Localizer["Architecture.Quality.Warnings"],
            Localizer["Architecture.Quality.EditorConfig"],
            Localizer["Architecture.Quality.Options"],
            Localizer["Architecture.Quality.StaticExport"],
            Localizer["Architecture.Quality.NoJs"],
        ];

        patterns = await Mediator.SendAsync(new GetArchitecturePatternsQuery());

        await Mediator.PublishAsync(new PageVisitedNotification("Architecture"));
    }

    private string BuildDocumentationUrl(ArchitecturePattern pattern) =>
        PortfolioOptions.Value.BuildDocumentationUrl(pattern.DocumentPath);
}
