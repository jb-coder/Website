using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Strategies;

/// <summary>Filters projects classified as mobile applications.</summary>
public sealed class MobileProjectsFilterStrategy : ProjectCategoryFilterStrategyBase
{
    protected override ProjectCategory Category => ProjectCategory.Mobile;
}
