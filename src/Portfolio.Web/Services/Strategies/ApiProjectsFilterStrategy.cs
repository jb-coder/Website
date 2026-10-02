using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Strategies;

/// <summary>Filters projects classified as APIs and services.</summary>
public sealed class ApiProjectsFilterStrategy : ProjectCategoryFilterStrategyBase
{
    protected override ProjectCategory Category => ProjectCategory.Api;
}
