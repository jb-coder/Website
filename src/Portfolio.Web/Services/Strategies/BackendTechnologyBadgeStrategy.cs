using Portfolio.Web.Services.Localization;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Strategies;

/// <summary>Badge strategy for backend technologies.</summary>
public sealed class BackendTechnologyBadgeStrategy(ITranslator localizer)
    : TechnologyBadgeStrategyBase(localizer)
{
    protected override TechnologyCategory Category => TechnologyCategory.Backend;

    protected override string LabelResourceKey => "TechnologyCategory.Backend";

    protected override string CssClass => "pf-badge--backend";
}
