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
        new("C# / .NET", TechnologyCategory.Backend, SkillLevel.Expert, "cpu"),
        new("ASP.NET Core", TechnologyCategory.Backend, SkillLevel.Expert, "server"),
        new("Minimal APIs", TechnologyCategory.Backend, SkillLevel.Expert, "zap"),
        new("REST & OpenAPI", TechnologyCategory.Backend, SkillLevel.Advanced, "globe"),
        new("gRPC", TechnologyCategory.Backend, SkillLevel.Advanced, "git-merge"),
        new("Entity Framework Core", TechnologyCategory.Backend, SkillLevel.Expert, "database"),
        new("SQL Server & PostgreSQL", TechnologyCategory.Backend, SkillLevel.Advanced, "hard-drive"),
        new("Redis", TechnologyCategory.Backend, SkillLevel.Intermediate, "layers"),

        // Frontend
        new("Blazor", TechnologyCategory.Frontend, SkillLevel.Expert, "layout"),
        new("Razor Components", TechnologyCategory.Frontend, SkillLevel.Expert, "code"),
        new("HTML5", TechnologyCategory.Frontend, SkillLevel.Advanced, "globe"),
        new("Modern CSS", TechnologyCategory.Frontend, SkillLevel.Advanced, "droplet"),
        new("TypeScript", TechnologyCategory.Frontend, SkillLevel.Intermediate, "terminal"),
        new("Accessibility (WCAG)", TechnologyCategory.Frontend, SkillLevel.Advanced, "eye"),
        new("Responsive Design", TechnologyCategory.Frontend, SkillLevel.Advanced, "monitor"),

        // Mobile
        new(".NET MAUI", TechnologyCategory.Mobile, SkillLevel.Expert, "smartphone"),
        new("Xamarin.Forms", TechnologyCategory.Mobile, SkillLevel.Expert, "smartphone"),
        new("MVVM", TechnologyCategory.Mobile, SkillLevel.Advanced, "grid"),
        new("Offline-first Sync", TechnologyCategory.Mobile, SkillLevel.Advanced, "refresh-cw"),
        new("SQLite", TechnologyCategory.Mobile, SkillLevel.Advanced, "database"),
        new("Barcode Scanning", TechnologyCategory.Mobile, SkillLevel.Advanced, "camera"),
        new("Store Publishing", TechnologyCategory.Mobile, SkillLevel.Intermediate, "upload-cloud"),

        // Cloud
        new("Azure Functions", TechnologyCategory.Cloud, SkillLevel.Advanced, "zap"),
        new("Azure SQL", TechnologyCategory.Cloud, SkillLevel.Advanced, "database"),
        new("Docker", TechnologyCategory.Cloud, SkillLevel.Intermediate, "box"),
        new("Kubernetes (AKS)", TechnologyCategory.Cloud, SkillLevel.Intermediate, "grid"),

        // Architecture
        new("Clean Architecture", TechnologyCategory.Architecture, SkillLevel.Expert, "layers"),
        new("CQRS", TechnologyCategory.Architecture, SkillLevel.Expert, "repeat"),
        new("Domain-Driven Design", TechnologyCategory.Architecture, SkillLevel.Advanced, "hexagon"),
        new("Event-Driven Architecture", TechnologyCategory.Architecture, SkillLevel.Advanced, "share-2"),
        new("SOLID Principles", TechnologyCategory.Architecture, SkillLevel.Expert, "check"),
        new("Design Patterns", TechnologyCategory.Architecture, SkillLevel.Expert, "grid"),
        new("Modular Monolith", TechnologyCategory.Architecture, SkillLevel.Advanced, "box"),
        new("Testing Strategy", TechnologyCategory.Architecture, SkillLevel.Advanced, "shield"),
    ];

    public Task<IReadOnlyList<Skill>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Skills);
    }
}
