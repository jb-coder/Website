using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Projects;

/// <summary>
/// Loads the catalogue, delegates filtering to the matching strategy and maps
/// the result through the project card factory.
/// </summary>
public sealed class GetProjectsQueryHandler(
    IProjectRepository repository,
    IEnumerable<IProjectFilterStrategy> filters,
    IProjectCardFactory factory)
    : IRequestHandler<GetProjectsQuery, IReadOnlyList<ProjectCardViewModel>>
{
    public async Task<IReadOnlyList<ProjectCardViewModel>> HandleAsync(
        GetProjectsQuery request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var projects = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);

        var filter = filters.FirstOrDefault(candidate => candidate.CanHandle(request.Category))
            ?? throw new InvalidOperationException(
                $"No project filter is registered for category '{request.Category}'.");

        return factory.CreateMany(filter.Apply(projects));
    }
}
