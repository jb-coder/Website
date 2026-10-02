using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;

namespace Portfolio.Web.Services.Repositories;

/// <summary>
/// In-memory skills catalogue, grouped by <see cref="TechnologyCategory"/>.
/// </summary>
public sealed class SkillRepository : ISkillRepository
{
    private static readonly IReadOnlyList<Skill> Skills =
    [
        // Backend
        new(SkillName("C# / .NET", "C# / .NET"), TechnologyCategory.Backend, SkillLevel.Expert, "cpu"),
        new(SkillName("ASP.NET Core", "ASP.NET Core"), TechnologyCategory.Backend, SkillLevel.Expert, "server"),
        new(SkillName("Minimal APIs", "Minimal APIs"), TechnologyCategory.Backend, SkillLevel.Expert, "zap"),
        new(SkillName("REST & OpenAPI", "REST & OpenAPI"), TechnologyCategory.Backend, SkillLevel.Advanced, "globe"),
        new(SkillName("gRPC", "gRPC"), TechnologyCategory.Backend, SkillLevel.Advanced, "git-merge"),
        new(SkillName("Entity Framework Core", "Entity Framework Core"), TechnologyCategory.Backend, SkillLevel.Expert, "database"),
        new(SkillName("SQL Server & PostgreSQL", "SQL Server & PostgreSQL"), TechnologyCategory.Backend, SkillLevel.Advanced, "hard-drive"),
        new(SkillName("Redis", "Redis"), TechnologyCategory.Backend, SkillLevel.Intermediate, "layers"),

        // Frontend
        new(SkillName("Blazor", "Blazor"), TechnologyCategory.Frontend, SkillLevel.Expert, "layout"),
        new(SkillName("Razor Components", "Razor Components"), TechnologyCategory.Frontend, SkillLevel.Expert, "code"),
        new(SkillName("HTML5", "HTML5"), TechnologyCategory.Frontend, SkillLevel.Advanced, "globe"),
        new(SkillName("CSS moderno", "Modern CSS"), TechnologyCategory.Frontend, SkillLevel.Advanced, "droplet"),
        new(SkillName("TypeScript", "TypeScript"), TechnologyCategory.Frontend, SkillLevel.Intermediate, "terminal"),
        new(SkillName("Accesibilidad (WCAG)", "Accessibility (WCAG)"), TechnologyCategory.Frontend, SkillLevel.Advanced, "eye"),
        new(SkillName("Diseño responsive", "Responsive Design"), TechnologyCategory.Frontend, SkillLevel.Advanced, "monitor"),

        // Mobile
        new(SkillName(".NET MAUI", ".NET MAUI"), TechnologyCategory.Mobile, SkillLevel.Expert, "smartphone"),
        new(SkillName("Xamarin.Forms", "Xamarin.Forms"), TechnologyCategory.Mobile, SkillLevel.Expert, "smartphone"),
        new(SkillName("MVVM", "MVVM"), TechnologyCategory.Mobile, SkillLevel.Advanced, "grid"),
        new(SkillName("Sincronización offline-first", "Offline-first Sync"), TechnologyCategory.Mobile, SkillLevel.Advanced, "refresh-cw"),
        new(SkillName("SQLite", "SQLite"), TechnologyCategory.Mobile, SkillLevel.Advanced, "database"),
        new(SkillName("Escaneo de códigos", "Barcode Scanning"), TechnologyCategory.Mobile, SkillLevel.Advanced, "camera"),
        new(SkillName("Publicación en stores", "Store Publishing"), TechnologyCategory.Mobile, SkillLevel.Intermediate, "upload-cloud"),

        // Cloud
        new(SkillName("Azure Functions", "Azure Functions"), TechnologyCategory.Cloud, SkillLevel.Advanced, "zap"),
        new(SkillName("Azure SQL", "Azure SQL"), TechnologyCategory.Cloud, SkillLevel.Advanced, "database"),
        new(SkillName("Docker", "Docker"), TechnologyCategory.Cloud, SkillLevel.Intermediate, "box"),
        new(SkillName("Kubernetes (AKS)", "Kubernetes (AKS)"), TechnologyCategory.Cloud, SkillLevel.Intermediate, "grid"),

        // Architecture
        new(SkillName("Clean Architecture", "Clean Architecture"), TechnologyCategory.Architecture, SkillLevel.Expert, "layers"),
        new(SkillName("CQRS", "CQRS"), TechnologyCategory.Architecture, SkillLevel.Expert, "repeat"),
        new(SkillName("Domain-Driven Design", "Domain-Driven Design"), TechnologyCategory.Architecture, SkillLevel.Advanced, "hexagon"),
        new(SkillName("Event-Driven Architecture", "Event-Driven Architecture"), TechnologyCategory.Architecture, SkillLevel.Advanced, "share-2"),
        new(SkillName("Principios SOLID", "SOLID Principles"), TechnologyCategory.Architecture, SkillLevel.Expert, "check"),
        new(SkillName("Patrones de diseño", "Design Patterns"), TechnologyCategory.Architecture, SkillLevel.Expert, "grid"),
        new(SkillName("Monolito modular", "Modular Monolith"), TechnologyCategory.Architecture, SkillLevel.Advanced, "box"),
        new(SkillName("Estrategia de testing", "Testing Strategy"), TechnologyCategory.Architecture, SkillLevel.Advanced, "shield"),
    ];

    public Task<IReadOnlyList<Skill>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Skills);
    }

    private static LocalizedText SkillName(string es, string en) => new(es, en);
}
