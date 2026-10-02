using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Strategies;

/// <summary>Badge strategy for cloud and DevOps technologies.</summary>
public sealed class CloudTechnologyBadgeStrategy : TechnologyBadgeStrategyBase
{
    protected override TechnologyCategory Category => TechnologyCategory.Cloud;

    protected override string Label => "Cloud";

    protected override string CssClass => "pf-badge--cloud";
}
