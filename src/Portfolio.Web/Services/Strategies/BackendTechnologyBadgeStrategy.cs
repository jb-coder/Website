using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Strategies;

/// <summary>Badge strategy for backend technologies.</summary>
public sealed class BackendTechnologyBadgeStrategy : TechnologyBadgeStrategyBase
{
    protected override TechnologyCategory Category => TechnologyCategory.Backend;

    protected override string Label => "Backend";

    protected override string CssClass => "pf-badge--backend";
}
