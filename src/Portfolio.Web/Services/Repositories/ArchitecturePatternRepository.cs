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
            Intent: new(
                "Invertir las dependencias de cada componente para que dependa de abstracciones, no de implementaciones.",
                "Invert the dependencies of every component so it depends on abstractions, not implementations."),
            Implementation: new(
                "Cada servicio se registra en el contenedor mediante la extensión AddPortfolio; páginas y componentes reciben sus colaboradores por inyección.",
                "Every service is registered in the container through the AddPortfolio extension; components and pages receive their collaborators through constructor or @inject."),
            Benefit: new(
                "Testabilidad, infraestructura sustituible y un único lugar donde decidir los ciclos de vida.",
                "Testability, replaceable infrastructure and a single place to reason about object lifetimes."),
            Icon: "git-branch",
            DocumentPath: "dependency-injection.md"),
        new ArchitecturePattern(
            Id: "options",
            Name: "Options Pattern",
            Intent: new(
                "Asociar secciones de configuración a objetos fuertemente tipados y validados.",
                "Bind configuration sections to strongly typed, validated objects."),
            Implementation: new(
                "PortfolioOptions se asocia a la sección Portfolio, con anotaciones de datos y validación al arranque (ValidateOnStart).",
                "PortfolioOptions is bound to the Portfolio section, annotated with data annotations and validated on startup (ValidateOnStart)."),
            Benefit: new(
                "Configuración tipada y de fallo temprano, sin cadenas mágicas en los puntos de uso.",
                "Typed, fail-fast configuration with IntelliSense and no magic strings at call sites."),
            Icon: "settings",
            DocumentPath: "options.md"),
        new ArchitecturePattern(
            Id: "repository",
            Name: "Repository Pattern",
            Intent: new(
                "Aislar el acceso a datos detrás de una abstracción que pertenece al núcleo de la aplicación.",
                "Isolate data access behind an abstraction owned by the application core."),
            Implementation: new(
                "IProjectRepository, ISkillRepository, IExperienceRepository e IArchitecturePatternRepository exponen modelos de dominio; hoy se inyectan implementaciones en memoria.",
                "IProjectRepository, ISkillRepository, IExperienceRepository and IArchitecturePatternRepository expose domain models; in-memory implementations are injected today."),
            Benefit: new(
                "La fuente de datos puede pasar a un CMS o una API sin tocar un solo componente.",
                "The data source can move to a CMS or API without touching a single component."),
            Icon: "database",
            DocumentPath: "repository.md"),
        new ArchitecturePattern(
            Id: "strategy",
            Name: "Strategy Pattern",
            Intent: new(
                "Seleccionar un algoritmo en tiempo de ejecución sin ramificaciones en el consumidor.",
                "Select an algorithm at runtime without branching in the consumer."),
            Implementation: new(
                "ITechnologyBadgeStrategy pinta cada categoría de tecnología e IProjectFilterStrategy resuelve el filtro de proyectos; ambas se resuelven desde DI.",
                "ITechnologyBadgeStrategy renders each technology category and IProjectFilterStrategy resolves the projects filter; both are resolved from DI."),
            Benefit: new(
                "Nuevas categorías o filtros se añaden con clases nuevas, respetando el principio Open/Closed.",
                "New categories or filters are added with new classes, honoring the Open/Closed Principle."),
            Icon: "share-2",
            DocumentPath: "strategy.md"),
        new ArchitecturePattern(
            Id: "factory",
            Name: "Factory Pattern",
            Intent: new(
                "Centralizar la creación y proyección de objetos complejos.",
                "Centralize complex object creation and projection."),
            Implementation: new(
                "ProjectCardFactory mapea el modelo de dominio Project a ProjectCardViewModel, componiendo los badges con la familia de estrategias.",
                "ProjectCardFactory maps the Project domain model to ProjectCardViewModel, composing badges through the strategy family."),
            Benefit: new(
                "Los componentes quedan declarativos y la lógica de mapeo vive en un único lugar.",
                "Components stay declarative and mapping logic lives in exactly one place."),
            Icon: "box",
            DocumentPath: "factory.md"),
        new ArchitecturePattern(
            Id: "builder",
            Name: "Builder Pattern",
            Intent: new(
                "Componer objetos complejos paso a paso con una API fluida y legible.",
                "Compose complex objects step by step with a readable, fluent API."),
            Implementation: new(
                "PortfolioSectionBuilder compone los metadatos de sección (id, eyebrow, título, subtítulo, clase CSS) para SectionHeader.",
                "PortfolioSectionBuilder composes section metadata (id, eyebrow, title, subtitle, CSS class) for SectionHeader."),
            Benefit: new(
                "Elimina parámetros de marcado repetidos y valida el resultado al llamar a Build().",
                "Removes repeated markup parameters and validates the result when Build() is called."),
            Icon: "layers",
            DocumentPath: "builder.md"),
        new ArchitecturePattern(
            Id: "mediator",
            Name: "Mediator (Light)",
            Intent: new(
                "Desacoplar las páginas de los servicios que resuelven sus casos de uso.",
                "Decouple pages from the services that fulfill their use cases."),
            Implementation: new(
                "Un mediator basado en reflexión despacha consultas y notificaciones a handlers registrados en DI, sin librerías externas.",
                "A reflection-based mediator dispatches queries and notifications to handler implementations registered in DI, without external libraries."),
            Benefit: new(
                "Las páginas expresan intención (GetProjectsQuery) y no saben de repositorios, caché ni analítica.",
                "Pages express intent (GetProjectsQuery) and remain unaware of repositories, caching or analytics."),
            Icon: "send",
            DocumentPath: "mediator.md"),
        new ArchitecturePattern(
            Id: "static-rendering",
            Name: "Static Rendering",
            Intent: new(
                "Publicar HTML ya renderizado que pueda alojarse en cualquier sitio.",
                "Ship fully pre-rendered HTML that can be hosted anywhere."),
            Implementation: new(
                "Blazor Web App con static SSR, sin modo interactivo ni JavaScript del framework; un exportador de consola captura cada ruta para GitHub Pages.",
                "Blazor Web App with static SSR, no interactive render mode and no framework JavaScript on the page; a console exporter snapshots every route for GitHub Pages."),
            Benefit: new(
                "Cero runtime, cargas instantáneas, SEO perfecto y puntuaciones Lighthouse altas.",
                "Zero runtime, instant loads, perfect SEO and Lighthouse scores."),
            Icon: "cloud",
            DocumentPath: "static-rendering.md"),
    ];

    public Task<IReadOnlyList<ArchitecturePattern>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Patterns);
    }
}
