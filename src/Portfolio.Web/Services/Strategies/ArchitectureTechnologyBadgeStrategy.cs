using Portfolio.Web.Services.Localization;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Strategies;

/// <summary>Badge strategy for architecture and design technologies.</summary>
public sealed class ArchitectureTechnologyBadgeStrategy(ITranslator localizer)
    : TechnologyBadgeStrategyBase(localizer)
{
    protected override TechnologyCategory Category => TechnologyCategory.Architecture;

    protected override string LabelResourceKey => "TechnologyCategory.Architecture";

    protected override string CssClass => "pf-badge--architecture";
}
