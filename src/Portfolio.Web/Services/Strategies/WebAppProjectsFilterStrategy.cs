using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Strategies;

/// <summary>Filters projects classified as web platforms.</summary>
public sealed class WebAppProjectsFilterStrategy : ProjectCategoryFilterStrategyBase
{
    protected override ProjectCategory Category => ProjectCategory.WebApp;
}
