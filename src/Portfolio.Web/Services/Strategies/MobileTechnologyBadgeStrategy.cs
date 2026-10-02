using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Strategies;

/// <summary>Badge strategy for mobile technologies.</summary>
public sealed class MobileTechnologyBadgeStrategy : TechnologyBadgeStrategyBase
{
    protected override TechnologyCategory Category => TechnologyCategory.Mobile;

    protected override string Label => "Mobile";

    protected override string CssClass => "pf-badge--mobile";
}
