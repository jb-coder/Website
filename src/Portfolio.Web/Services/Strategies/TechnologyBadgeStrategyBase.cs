using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;

namespace Portfolio.Web.Services.Strategies;

/// <summary>
/// Shared implementation for badge strategies. Concrete strategies only declare
/// their category, display label and CSS modifier (Open/Closed Principle).
/// </summary>
public abstract class TechnologyBadgeStrategyBase : ITechnologyBadgeStrategy
{
    protected abstract TechnologyCategory Category { get; }

    protected abstract string Label { get; }

    protected abstract string CssClass { get; }

    public bool CanHandle(TechnologyCategory category) => category == Category;

    public TechnologyBadgeViewModel Create(Technology technology)
    {
        ArgumentNullException.ThrowIfNull(technology);

        return new TechnologyBadgeViewModel(
            technology.Name,
            Label,
            CssClass,
            technology.Icon,
            technology.Accent,
            $"{technology.Name}, {Label}");
    }
}
