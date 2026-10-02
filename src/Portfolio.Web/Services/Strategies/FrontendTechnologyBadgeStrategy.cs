using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Strategies;

/// <summary>Badge strategy for frontend technologies.</summary>
public sealed class FrontendTechnologyBadgeStrategy : TechnologyBadgeStrategyBase
{
    protected override TechnologyCategory Category => TechnologyCategory.Frontend;

    protected override string Label => "Frontend";

    protected override string CssClass => "pf-badge--frontend";
}
