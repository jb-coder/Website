using Portfolio.Web.Models;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Career;

/// <summary>
/// Retrieves the professional timeline entries.
/// </summary>
public sealed record GetCareerTimelineQuery : IRequest<IReadOnlyList<Experience>>;
