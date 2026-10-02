using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Skills;

/// <summary>
/// Retrieves the skills matrix grouped by technology category.
/// </summary>
public sealed record GetSkillGroupsQuery : IRequest<IReadOnlyList<SkillGroupViewModel>>;
