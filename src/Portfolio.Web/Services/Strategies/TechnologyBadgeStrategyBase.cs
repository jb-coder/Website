using Portfolio.Web.Services.Localization;
using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;

namespace Portfolio.Web.Services.Strategies;

/// <summary>
/// Shared implementation for badge strategies. Concrete strategies only declare
/// their category, resource key and CSS modifier (Open/Closed Principle).
/// </summary>
public abstract class TechnologyBadgeStrategyBase(ITranslator localizer)
    : ITechnologyBadgeStrategy
{
    protected abstract TechnologyCategory Category { get; }

    protected abstract string LabelResourceKey { get; }

    protected abstract string CssClass { get; }

    public bool CanHandle(TechnologyCategory category) => category == Category;

    public TechnologyBadgeViewModel Create(Technology technology)
    {
        ArgumentNullException.ThrowIfNull(technology);

        var label = localizer[LabelResourceKey];

        return new TechnologyBadgeViewModel(
            technology.Name,
            label,
            CssClass,
            technology.Icon,
            technology.Accent,
            $"{technology.Name}, {label}");
    }
}
