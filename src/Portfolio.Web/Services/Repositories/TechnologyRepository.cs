using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Shared;

namespace Portfolio.Web.Services.Repositories;

/// <summary>
/// Curated core stack shown in the home marquee.
/// </summary>
public sealed class TechnologyRepository : ITechnologyRepository
{
    private static readonly IReadOnlyList<Technology> CoreTechnologies =
    [
        new(".NET", TechnologyCategory.Backend, "cpu", PortfolioAccents.Backend),
        new("C#", TechnologyCategory.Backend, "code", PortfolioAccents.Backend),
        new("ASP.NET Core", TechnologyCategory.Backend, "server", PortfolioAccents.Backend),
        new("Minimal APIs", TechnologyCategory.Backend, "zap", PortfolioAccents.Backend),
        new("Entity Framework Core", TechnologyCategory.Backend, "database", PortfolioAccents.Backend),
        new("SQL Server", TechnologyCategory.Backend, "hard-drive", PortfolioAccents.Backend),
        new("Blazor", TechnologyCategory.Frontend, "layout", PortfolioAccents.Frontend),
        new("Modern CSS", TechnologyCategory.Frontend, "droplet", PortfolioAccents.Frontend),
        new(".NET MAUI", TechnologyCategory.Mobile, "smartphone", PortfolioAccents.Mobile),
        new("Azure", TechnologyCategory.Cloud, "cloud", PortfolioAccents.Cloud),
        new("Azure Functions", TechnologyCategory.Cloud, "zap", PortfolioAccents.Cloud),
        new("Docker", TechnologyCategory.Cloud, "box", PortfolioAccents.Cloud),
        new("Azure DevOps", TechnologyCategory.Cloud, "git-branch", PortfolioAccents.Cloud),
        new("Clean Architecture", TechnologyCategory.Architecture, "layers", PortfolioAccents.Architecture),
        new("CQRS", TechnologyCategory.Architecture, "repeat", PortfolioAccents.Architecture),
        new("DDD", TechnologyCategory.Architecture, "hexagon", PortfolioAccents.Architecture),
    ];

    public Task<IReadOnlyList<Technology>> GetCoreAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(CoreTechnologies);
    }
}
