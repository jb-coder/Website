using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Career;

/// <summary>
/// Returns the timeline ordered from most recent to oldest.
/// </summary>
public sealed class GetCareerTimelineQueryHandler(IExperienceRepository repository)
    : IRequestHandler<GetCareerTimelineQuery, IReadOnlyList<Experience>>
{
    public async Task<IReadOnlyList<Experience>> HandleAsync(
        GetCareerTimelineQuery request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var entries = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);

        return entries
            .OrderByDescending(entry => entry.StartDate)
            .ToArray();
    }
}
