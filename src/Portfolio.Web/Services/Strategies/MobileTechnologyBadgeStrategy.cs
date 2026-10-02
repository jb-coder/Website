using Portfolio.Web.Services.Localization;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Strategies;

/// <summary>Badge strategy for mobile technologies.</summary>
public sealed class MobileTechnologyBadgeStrategy(ITranslator localizer)
    : TechnologyBadgeStrategyBase(localizer)
{
    protected override TechnologyCategory Category => TechnologyCategory.Mobile;

    protected override string LabelResourceKey => "TechnologyCategory.Mobile";

    protected override string CssClass => "pf-badge--mobile";
}
