using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;

namespace Portfolio.Web.Services.Repositories;

/// <summary>
/// Catalogue of the design patterns applied in this very solution. Each entry
/// links to its dedicated document under <c>Documentation/</c>.
/// </summary>
public sealed class ArchitecturePatternRepository : IArchitecturePatternRepository
{
    private static readonly IReadOnlyList<ArchitecturePattern> Patterns =
    [
        new ArchitecturePattern(
            Id: "dependency-injection",
            Name: "Dependency Injection",
            Intent: "Invert the dependencies of every component so it depends on abstractions, not implementations.",
            Implementation: "Every service is registered in the container through the AddPortfolio extension; components and pages receive their collaborators through constructor or @inject.",
            Benefit: "Testability, replaceable infrastructure and a single place to reason about object lifetimes.",
            Icon: "git-branch",
            DocumentPath: "dependency-injection.md"),
        new ArchitecturePattern(
            Id: "options",
            Name: "Options Pattern",
            Intent: "Bind configuration sections to strongly typed, validated objects.",
            Implementation: "PortfolioOptions is bound to the Portfolio section, annotated with data annotations and validated on startup (ValidateOnStart).",
            Benefit: "Typed, fail-fast configuration with IntelliSense and no magic strings at call sites.",
            Icon: "settings",
            DocumentPath: "options.md"),
        new ArchitecturePattern(
            Id: "repository",
            Name: "Repository Pattern",
            Intent: "Isolate data access behind an abstraction owned by the application core.",
            Implementation: "IProjectRepository, ISkillRepository, IExperienceRepository and IArchitecturePatternRepository expose domain models; in-memory implementations are injected today.",
            Benefit: "The data source can move to a CMS or API without touching a single component.",
            Icon: "database",
            DocumentPath: "repository.md"),
        new ArchitecturePattern(
            Id: "strategy",
            Name: "Strategy Pattern",
            Intent: "Select an algorithm at runtime without branching in the consumer.",
            Implementation: "ITechnologyBadgeStrategy renders each technology category and IProjectFilterStrategy resolves the projects filter; both are resolved from DI.",
            Benefit: "New categories or filters are added with new classes, honoring the Open/Closed Principle.",
            Icon: "share-2",
            DocumentPath: "strategy.md"),
        new ArchitecturePattern(
            Id: "factory",
            Name: "Factory Pattern",
            Intent: "Centralize complex object creation and projection.",
            Implementation: "ProjectCardFactory maps the Project domain model to ProjectCardViewModel, composing badges through the strategy family.",
            Benefit: "Components stay declarative and mapping logic lives in exactly one place.",
            Icon: "box",
            DocumentPath: "factory.md"),
        new ArchitecturePattern(
            Id: "builder",
            Name: "Builder Pattern",
            Intent: "Compose complex objects step by step with a readable, fluent API.",
            Implementation: "PortfolioSectionBuilder composes section metadata (id, eyebrow, title, subtitle, CSS class) for SectionHeader.",
            Benefit: "Removes repeated markup parameters and validates the result when Build() is called.",
            Icon: "layers",
            DocumentPath: "builder.md"),
        new ArchitecturePattern(
            Id: "mediator",
            Name: "Mediator (Light)",
            Intent: "Decouple pages from the services that fulfill their use cases.",
            Implementation: "A reflection-based mediator dispatches queries and notifications to handler implementations registered in DI, without external libraries.",
            Benefit: "Pages express intent (GetProjectsQuery) and remain unaware of repositories, caching or analytics.",
            Icon: "send",
            DocumentPath: "mediator.md"),
        new ArchitecturePattern(
            Id: "static-rendering",
            Name: "Static Rendering",
            Intent: "Ship fully pre-rendered HTML that can be hosted anywhere.",
            Implementation: "Blazor Web App with static SSR, no interactive render mode and no framework JavaScript on the page; a console exporter snapshots every route for GitHub Pages.",
            Benefit: "Zero runtime, instant loads, perfect SEO and Lighthouse scores.",
            Icon: "cloud",
            DocumentPath: "static-rendering.md"),
    ];

    public Task<IReadOnlyList<ArchitecturePattern>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Patterns);
    }
}
