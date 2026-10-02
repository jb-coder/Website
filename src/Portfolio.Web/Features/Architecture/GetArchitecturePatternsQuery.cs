using Portfolio.Web.Models;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Architecture;

/// <summary>
/// Retrieves the design patterns documented in this solution.
/// </summary>
public sealed record GetArchitecturePatternsQuery : IRequest<IReadOnlyList<ArchitecturePattern>>;
