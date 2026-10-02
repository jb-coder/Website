using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Abstractions;

/// <summary>
/// Read access to portfolio projects. The abstraction keeps the data source
/// replaceable (static seed today, CMS or API tomorrow).
/// </summary>
public interface IProjectRepository
{
    Task<IReadOnlyList<Project>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Project?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
}
