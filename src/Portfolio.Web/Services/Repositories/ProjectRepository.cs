using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;

namespace Portfolio.Web.Services.Repositories;

/// <summary>
/// In-memory project catalogue. Stateless and immutable, therefore registered
/// as a singleton. Swapping it for a CMS or API client only requires a new
/// <see cref="IProjectRepository"/> implementation.
/// </summary>
public sealed class ProjectRepository : IProjectRepository
{
    private static readonly IReadOnlyList<Project> Projects =
    [
        new Project(
            Id: "erp-platform",
            Title: "ERP Platform",
            Summary: "Modular ERP under construction to unify operations, logistics and finance.",
            Description: "Greenfield ERP that centralizes day-to-day operations across departments. Built as a modular platform so each business area can evolve independently, with a strong focus on data integrity, role-based access and process automation.",
            Category: ProjectCategory.WebApp,
            Status: ProjectStatus.InProgress,
            Year: 2025,
            Accent: "#58A6FF",
            Icon: "layers",
            IsFeatured: true,
            Highlights:
            [
                "Under active construction, module by module",
                "Modular architecture ready to grow per business area",
                "Automation of core operational processes",
            ],
            Technologies:
            [
                Tech(".NET", TechnologyCategory.Backend, "cpu", "#58A6FF"),
                Tech("ASP.NET Core", TechnologyCategory.Backend, "server", "#79C0FF"),
                Tech("EF Core", TechnologyCategory.Backend, "database", "#A5D6FF"),
                Tech("SQL Server", TechnologyCategory.Backend, "hard-drive", "#58A6FF"),
                Tech("Angular", TechnologyCategory.Frontend, "code", "#79C0FF"),
                Tech("Clean Architecture", TechnologyCategory.Architecture, "layers", "#A5D6FF"),
                Tech("AWS", TechnologyCategory.Cloud, "cloud", "#58A6FF"),
            ]),
        new Project(
            Id: "field-operations-app",
            Title: "Field Operations App",
            Summary: "MAUI application for field technicians to manage work orders and inspections.",
            Description: "Cross-platform app that puts daily work orders, digital checklists and evidence capture in the hands of field teams. Built with .NET MAUI and an offline-tolerant data layer that synchronizes with the backend as soon as connectivity is available.",
            Category: ProjectCategory.Mobile,
            Status: ProjectStatus.InProgress,
            Year: 2025,
            Accent: "#79C0FF",
            Icon: "smartphone",
            IsFeatured: true,
            Highlights:
            [
                "Digital checklists replace paper-based processes",
                "Photo and signature evidence attached to every order",
                "Offline-tolerant synchronization for field work",
            ],
            Technologies:
            [
                Tech(".NET MAUI", TechnologyCategory.Mobile, "smartphone", "#58A6FF"),
                Tech("C#", TechnologyCategory.Backend, "code", "#79C0FF"),
                Tech("REST", TechnologyCategory.Backend, "globe", "#A5D6FF"),
                Tech("SQLite", TechnologyCategory.Mobile, "database", "#58A6FF"),
                Tech("AWS", TechnologyCategory.Cloud, "cloud", "#79C0FF"),
            ]),
    ];

    public Task<IReadOnlyList<Project>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Projects);
    }

    public Task<Project?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        cancellationToken.ThrowIfCancellationRequested();

        var project = Projects.FirstOrDefault(candidate =>
            string.Equals(candidate.Id, id, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(project);
    }

    private static Technology Tech(string name, TechnologyCategory category, string icon, string accent) =>
        new(name, category, icon, accent);
}
