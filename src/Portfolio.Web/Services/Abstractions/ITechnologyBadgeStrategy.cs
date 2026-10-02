using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;

namespace Portfolio.Web.Services.Abstractions;

/// <summary>
/// Strategy Pattern: each implementation knows how to render badges for one
/// <see cref="TechnologyCategory"/>. New categories are added without touching
/// the factory or the UI.
/// </summary>
public interface ITechnologyBadgeStrategy
{
    bool CanHandle(TechnologyCategory category);

    TechnologyBadgeViewModel Create(Technology technology);
}
