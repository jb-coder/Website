using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Home;

/// <summary>
/// Loads the curated stack and resolves each badge through its category
/// strategy.
/// </summary>
public sealed class GetCoreTechnologiesQueryHandler(
    ITechnologyRepository repository,
    IEnumerable<ITechnologyBadgeStrategy> badgeStrategies)
    : IRequestHandler<GetCoreTechnologiesQuery, IReadOnlyList<TechnologyBadgeViewModel>>
{
    public async Task<IReadOnlyList<TechnologyBadgeViewModel>> HandleAsync(
        GetCoreTechnologiesQuery request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var technologies = await repository.GetCoreAsync(cancellationToken).ConfigureAwait(false);

        return technologies.Select(CreateBadge).ToArray();
    }

    private TechnologyBadgeViewModel CreateBadge(Technology technology)
    {
        var strategy = badgeStrategies.FirstOrDefault(candidate => candidate.CanHandle(technology.Category))
            ?? throw new InvalidOperationException(
                $"No badge strategy is registered for category '{technology.Category}'.");

        return strategy.Create(technology);
    }
}
