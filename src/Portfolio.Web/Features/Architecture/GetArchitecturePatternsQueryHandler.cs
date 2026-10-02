using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Architecture;

/// <summary>
/// Returns the documented patterns in catalogue order.
/// </summary>
public sealed class GetArchitecturePatternsQueryHandler(IArchitecturePatternRepository repository)
    : IRequestHandler<GetArchitecturePatternsQuery, IReadOnlyList<ArchitecturePattern>>
{
    public Task<IReadOnlyList<ArchitecturePattern>> HandleAsync(
        GetArchitecturePatternsQuery request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return repository.GetAllAsync(cancellationToken);
    }
}
