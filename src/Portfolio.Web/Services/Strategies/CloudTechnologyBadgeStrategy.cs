using Portfolio.Web.Services.Localization;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Strategies;

/// <summary>Badge strategy for cloud and DevOps technologies.</summary>
public sealed class CloudTechnologyBadgeStrategy(ITranslator localizer)
    : TechnologyBadgeStrategyBase(localizer)
{
    protected override TechnologyCategory Category => TechnologyCategory.Cloud;

    protected override string LabelResourceKey => "TechnologyCategory.Cloud";

    protected override string CssClass => "pf-badge--cloud";
}
