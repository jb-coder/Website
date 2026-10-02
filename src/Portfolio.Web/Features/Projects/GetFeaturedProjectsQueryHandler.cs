using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Projects;

/// <summary>
/// Selects the featured projects, most recent first.
/// </summary>
public sealed class GetFeaturedProjectsQueryHandler(
    IProjectRepository repository,
    IProjectCardFactory factory)
    : IRequestHandler<GetFeaturedProjectsQuery, IReadOnlyList<ProjectCardViewModel>>
{
    public async Task<IReadOnlyList<ProjectCardViewModel>> HandleAsync(
        GetFeaturedProjectsQuery request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var projects = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);

        var featured = projects
            .Where(project => project.IsFeatured)
            .OrderByDescending(project => project.Year)
            .Take(Math.Max(0, request.Take))
            .ToArray();

        return factory.CreateMany(featured);
    }
}
