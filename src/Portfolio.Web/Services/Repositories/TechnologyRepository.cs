using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;

namespace Portfolio.Web.Services.Repositories;

/// <summary>
/// Curated core stack shown in the home marquee.
/// </summary>
public sealed class TechnologyRepository : ITechnologyRepository
{
    private static readonly IReadOnlyList<Technology> CoreTechnologies =
    [
        new(".NET", TechnologyCategory.Backend, "cpu", "#58A6FF"),
        new("C#", TechnologyCategory.Backend, "code", "#79C0FF"),
        new("ASP.NET Core", TechnologyCategory.Backend, "server", "#A5D6FF"),
        new("Minimal APIs", TechnologyCategory.Backend, "zap", "#58A6FF"),
        new("Entity Framework Core", TechnologyCategory.Backend, "database", "#79C0FF"),
        new("SQL Server", TechnologyCategory.Backend, "hard-drive", "#A5D6FF"),
        new("Blazor", TechnologyCategory.Frontend, "layout", "#58A6FF"),
        new("Modern CSS", TechnologyCategory.Frontend, "droplet", "#79C0FF"),
        new(".NET MAUI", TechnologyCategory.Mobile, "smartphone", "#A5D6FF"),
        new("Azure", TechnologyCategory.Cloud, "cloud", "#58A6FF"),
        new("Azure Functions", TechnologyCategory.Cloud, "zap", "#79C0FF"),
        new("Docker", TechnologyCategory.Cloud, "box", "#A5D6FF"),
        new("Azure DevOps", TechnologyCategory.Cloud, "git-branch", "#58A6FF"),
        new("Clean Architecture", TechnologyCategory.Architecture, "layers", "#79C0FF"),
        new("CQRS", TechnologyCategory.Architecture, "repeat", "#A5D6FF"),
        new("DDD", TechnologyCategory.Architecture, "hexagon", "#58A6FF"),
    ];

    public Task<IReadOnlyList<Technology>> GetCoreAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(CoreTechnologies);
    }
}
