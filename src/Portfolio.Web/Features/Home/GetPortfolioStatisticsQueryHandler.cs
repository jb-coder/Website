using Microsoft.Extensions.Options;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Mediator;
using Portfolio.Web.Services.Options;

namespace Portfolio.Web.Features.Home;

/// <summary>
/// Composes the metrics from the validated options and the repositories, so the
/// numbers never drift from the actual content.
/// </summary>
public sealed class GetPortfolioStatisticsQueryHandler(
    IOptions<PortfolioOptions> options,
    ISkillRepository skillRepository,
    IArchitecturePatternRepository patternRepository)
    : IRequestHandler<GetPortfolioStatisticsQuery, IReadOnlyList<StatsCardViewModel>>
{
    public async Task<IReadOnlyList<StatsCardViewModel>> HandleAsync(
        GetPortfolioStatisticsQuery request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var portfolio = options.Value;
        var skills = await skillRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        var patterns = await patternRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);

        return
        [
            new StatsCardViewModel(
                $"{portfolio.YearsOfExperience}+",
                "Years of experience",
                "Building production .NET systems",
                "briefcase",
                "#58A6FF"),
            new StatsCardViewModel(
                $"{portfolio.CompletedProjects}+",
                "Projects delivered",
                "APIs, platforms and mobile apps",
                "box",
                "#79C0FF"),
            new StatsCardViewModel(
                $"{skills.Count}",
                "Technologies in use",
                "Across backend, frontend, mobile and cloud",
                "cpu",
                "#A5D6FF"),
            new StatsCardViewModel(
                $"{patterns.Count}",
                "Patterns documented",
                "Applied and explained in this very site",
                "layers",
                "#6CB6FF"),
        ];
    }
}
