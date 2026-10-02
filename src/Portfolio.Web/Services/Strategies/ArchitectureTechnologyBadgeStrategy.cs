using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Strategies;

/// <summary>Badge strategy for architecture and design technologies.</summary>
public sealed class ArchitectureTechnologyBadgeStrategy : TechnologyBadgeStrategyBase
{
    protected override TechnologyCategory Category => TechnologyCategory.Architecture;

    protected override string Label => "Architecture";

    protected override string CssClass => "pf-badge--architecture";
}
