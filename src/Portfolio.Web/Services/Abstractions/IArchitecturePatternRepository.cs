using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Abstractions;

/// <summary>
/// Read access to the design patterns documented and applied in this solution.
/// </summary>
public interface IArchitecturePatternRepository
{
    Task<IReadOnlyList<ArchitecturePattern>> GetAllAsync(CancellationToken cancellationToken = default);
}
