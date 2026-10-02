using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Skills;

/// <summary>
/// Groups skills and resolves the proficiency mapping. Category metadata lives
/// here so the presentation layer only iterates results.
/// </summary>
public sealed class GetSkillGroupsQueryHandler(ISkillRepository repository)
    : IRequestHandler<GetSkillGroupsQuery, IReadOnlyList<SkillGroupViewModel>>
{
    public async Task<IReadOnlyList<SkillGroupViewModel>> HandleAsync(
        GetSkillGroupsQuery request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var skills = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);

        return skills
            .GroupBy(skill => skill.Category)
            .Select(group => CreateGroup(group.Key, group))
            .ToArray();
    }

    private static SkillGroupViewModel CreateGroup(
        TechnologyCategory category,
        IEnumerable<Skill> skills)
    {
        var (title, description, icon, accent) = DescribeCategory(category);

        return new SkillGroupViewModel(
            Id: category.ToString().ToLowerInvariant(),
            Title: title,
            Description: description,
            Icon: icon,
            Accent: accent,
            Skills: skills.Select(CreateItem).ToArray());
    }

    private static SkillItemViewModel CreateItem(Skill skill)
    {
        var (label, percentage, cssClass) = skill.Level switch
        {
            SkillLevel.Expert => ("Expert", 92, "pf-level--expert"),
            SkillLevel.Advanced => ("Advanced", 76, "pf-level--advanced"),
            _ => ("Intermediate", 58, "pf-level--intermediate"),
        };

        return new SkillItemViewModel(skill.Name, skill.Icon, label, percentage, cssClass);
    }

    private static (string Title, string Description, string Icon, string Accent) DescribeCategory(
        TechnologyCategory category) => category switch
        {
            TechnologyCategory.Backend => (
                "Backend",
                "Services, APIs and data access built for throughput and maintainability.",
                "server",
                "#58A6FF"),
            TechnologyCategory.Frontend => (
                "Frontend",
                "Server-rendered UI, component design and accessible experiences.",
                "layout",
                "#79C0FF"),
            TechnologyCategory.Mobile => (
                "Mobile",
                "Cross-platform apps that keep working when the network does not.",
                "smartphone",
                "#A5D6FF"),
            TechnologyCategory.Cloud => (
                "Cloud & DevOps",
                "Automated delivery and resilient hosting on Azure.",
                "cloud",
                "#6CB6FF"),
            _ => (
                "Architecture",
                "The principles and patterns that keep systems changeable.",
                "layers",
                "#388BFD"),
        };
}
