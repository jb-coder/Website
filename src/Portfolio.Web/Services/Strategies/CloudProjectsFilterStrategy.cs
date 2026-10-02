using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Strategies;

/// <summary>Filters projects classified as cloud and DevOps.</summary>
public sealed class CloudProjectsFilterStrategy : ProjectCategoryFilterStrategyBase
{
    protected override ProjectCategory Category => ProjectCategory.Cloud;
}
