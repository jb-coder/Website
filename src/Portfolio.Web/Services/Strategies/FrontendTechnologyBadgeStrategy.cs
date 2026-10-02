using Portfolio.Web.Services.Localization;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Strategies;

/// <summary>Badge strategy for frontend technologies.</summary>
public sealed class FrontendTechnologyBadgeStrategy(ITranslator localizer)
    : TechnologyBadgeStrategyBase(localizer)
{
    protected override TechnologyCategory Category => TechnologyCategory.Frontend;

    protected override string LabelResourceKey => "TechnologyCategory.Frontend";

    protected override string CssClass => "pf-badge--frontend";
}
