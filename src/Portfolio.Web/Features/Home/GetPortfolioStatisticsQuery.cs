using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Home;

/// <summary>
/// Retrieves the headline metrics shown in the hero section.
/// </summary>
public sealed record GetPortfolioStatisticsQuery : IRequest<IReadOnlyList<StatsCardViewModel>>;
