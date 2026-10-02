using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Projects;

/// <summary>
/// Retrieves the project catalogue, optionally filtered by category.
/// </summary>
public sealed record GetProjectsQuery(ProjectCategory? Category = null)
    : IRequest<IReadOnlyList<ProjectCardViewModel>>;
