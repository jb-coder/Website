using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Abstractions;

/// <summary>
/// Read access to the professional timeline entries.
/// </summary>
public interface IExperienceRepository
{
    Task<IReadOnlyList<Experience>> GetAllAsync(CancellationToken cancellationToken = default);
}
