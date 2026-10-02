using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Abstractions;

/// <summary>
/// Read access to the curated list of core technologies highlighted on the
/// home page.
/// </summary>
public interface ITechnologyRepository
{
    Task<IReadOnlyList<Technology>> GetCoreAsync(CancellationToken cancellationToken = default);
}
