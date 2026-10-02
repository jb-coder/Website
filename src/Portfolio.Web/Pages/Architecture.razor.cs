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
    private static readonly (string Icon, string Title, string Text, string[] Examples)[] Layers =
    [
        (
            "layout",
            "Presentation",
            "Razor components and pages render HTML only. They own no business decisions.",
            ["Layouts", "Pages", "Shared components"]),
        (
            "send",
            "Application",
            "Pages send queries through the mediator; handlers orchestrate the use case.",
            ["IMediator", "Queries", "Handlers"]),
        (
            "grid",
            "Domain services",
            "Strategies, factories and builders turn domain data into presentation models.",
            ["Strategies", "ProjectCardFactory", "SectionBuilder"]),
        (
            "database",
            "Infrastructure",
            "Repositories provide the data behind interfaces, today from static catalogues.",
            ["Repositories", "Options", "Configuration"]),
    ];

    private static readonly (string Letter, string Name, string Text)[] SolidPrinciples =
    [
        ("S", "Single responsibility", "One reason to change per class: strategies render, factories map, repositories supply data."),
        ("O", "Open/Closed", "New technology categories only require a new strategy; no existing class is modified."),
        ("L", "Liskov substitution", "Every repository and strategy implementation honours its interface contract."),
        ("I", "Interface segregation", "Small, purpose-specific interfaces such as IProjectRepository or ISkillRepository."),
        ("D", "Dependency inversion", "Pages depend on IMediator and services depend on abstractions, never on concretions."),
    ];

    private static readonly string[] QualityGates =
    [
        "Nullable reference types enabled",
        "TreatWarningsAsErrors = true",
        "EditorConfig style enforced at build time",
        "Options validated on startup (fail fast)",
        "Static export verified route by route",
        "No JavaScript required to navigate",
    ];

    private string pageTitle = string.Empty;
    private IReadOnlyList<ArchitecturePattern> patterns = [];
    private PortfolioSection layersSection = default!;
    private PortfolioSection diagramSection = default!;
    private PortfolioSection patternsSection = default!;
    private PortfolioSection solidSection = default!;
    private PortfolioSection qualitySection = default!;

    [Inject]
    private IMediator Mediator { get; set; } = default!;

    [Inject]
    private IOptions<PortfolioOptions> PortfolioOptions { get; set; } = default!;

    [Inject]
    private IPortfolioSectionBuilder SectionBuilder { get; set; } = default!;

    private IReadOnlyList<(string Icon, string Title, string Text, string[] Examples)> layers = Layers;
    private IReadOnlyList<(string Letter, string Name, string Text)> solidPrinciples = SolidPrinciples;
    private IReadOnlyList<string> qualityGates = QualityGates;

    protected override async Task OnInitializedAsync()
    {
        pageTitle = "Architecture — Patterns and decisions behind this site";

        layersSection = SectionBuilder
            .WithId("request-flow")
            .WithEyebrow("Layers")
            .WithTitle("From a click to the data and back")
            .WithSubtitle("A request flows downward through four layers, each one replaceable.")
            .Build();

        diagramSection = SectionBuilder
            .WithId("dependency-diagram")
            .WithEyebrow("Diagram")
            .WithTitle("Dependencies point inward")
            .WithSubtitle("The mediator keeps pages away from infrastructure details.")
            .Build();

        patternsSection = SectionBuilder
            .WithId("patterns")
            .WithEyebrow("Patterns")
            .WithTitle("Design patterns applied in production code")
            .WithSubtitle("Not a checklist: each pattern solves a concrete problem in this repository.")
            .Build();

        solidSection = SectionBuilder
            .WithId("solid")
            .WithEyebrow("SOLID")
            .WithTitle("Five principles, verified in the code")
            .WithSubtitle("Concrete evidence from this codebase for every letter.")
            .Build();

        qualitySection = SectionBuilder
            .WithId("quality")
            .WithEyebrow("Quality")
            .WithTitle("Gates enforced by the build")
            .WithSubtitle("If a rule matters, it fails the build instead of living in a wiki.")
            .Build();

        patterns = await Mediator.SendAsync(new GetArchitecturePatternsQuery());

        await Mediator.PublishAsync(new PageVisitedNotification("Architecture"));
    }

    private string BuildDocumentationUrl(ArchitecturePattern pattern) =>
        PortfolioOptions.Value.BuildDocumentationUrl(pattern.DocumentPath);
}
