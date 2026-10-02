using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Projects;

/// <summary>
/// Retrieves the highlighted projects shown on the home page.
/// </summary>
public sealed record GetFeaturedProjectsQuery(int Take = 3)
    : IRequest<IReadOnlyList<ProjectCardViewModel>>;
