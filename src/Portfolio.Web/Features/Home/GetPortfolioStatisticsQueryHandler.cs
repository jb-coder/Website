using Portfolio.Web.Services.Localization;
using Microsoft.Extensions.Options;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Mediator;
using Portfolio.Web.Services.Options;

namespace Portfolio.Web.Features.Home;

/// <summary>
/// Composes the metrics from the validated options and the repositories, so the
/// numbers never drift from the actual content. Labels are localized through
/// the shared resources.
/// </summary>
public sealed class GetPortfolioStatisticsQueryHandler(
    IOptions<PortfolioOptions> options,
    ISkillRepository skillRepository,
    IArchitecturePatternRepository patternRepository,
    ITranslator localizer)
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
                localizer["Stats.Years.Label"],
                localizer["Stats.Years.Description"],
                "briefcase",
                "#58A6FF"),
            new StatsCardViewModel(
                $"{portfolio.CompletedProjects}+",
                localizer["Stats.Projects.Label"],
                localizer["Stats.Projects.Description"],
                "box",
                "#79C0FF"),
            new StatsCardViewModel(
                $"{skills.Count}",
                localizer["Stats.Technologies.Label"],
                localizer["Stats.Technologies.Description"],
                "cpu",
                "#A5D6FF"),
            new StatsCardViewModel(
                $"{patterns.Count}",
                localizer["Stats.Patterns.Label"],
                localizer["Stats.Patterns.Description"],
                "layers",
                "#6CB6FF"),
        ];
    }
}
