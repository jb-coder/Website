using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Abstractions;

/// <summary>
/// Read access to the skills matrix.
/// </summary>
public interface ISkillRepository
{
    Task<IReadOnlyList<Skill>> GetAllAsync(CancellationToken cancellationToken = default);
}
