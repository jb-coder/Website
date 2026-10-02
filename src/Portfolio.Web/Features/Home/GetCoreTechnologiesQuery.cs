using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Home;

/// <summary>
/// Retrieves the core technology stack rendered as badges.
/// </summary>
public sealed record GetCoreTechnologiesQuery : IRequest<IReadOnlyList<TechnologyBadgeViewModel>>;
