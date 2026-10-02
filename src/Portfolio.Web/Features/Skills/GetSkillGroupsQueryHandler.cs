using Portfolio.Web.Models;
using Portfolio.Web.Models.ViewModels;
using Portfolio.Web.Services.Abstractions;
using Portfolio.Web.Services.Localization;
using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Skills;

/// <summary>
/// Groups skills and resolves the bilingual names, proficiency labels and
/// category metadata, so the presentation layer only iterates results.
/// </summary>
public sealed class GetSkillGroupsQueryHandler(
    ISkillRepository repository,
    ILanguageContext language,
    ITranslator localizer)
    : IRequestHandler<GetSkillGroupsQuery, IReadOnlyList<SkillGroupViewModel>>
{
    public async Task<IReadOnlyList<SkillGroupViewModel>> HandleAsync(
        GetSkillGroupsQuery request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var skills = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        var current = language.Language;

        return skills
            .GroupBy(skill => skill.Category)
            .Select(group => CreateGroup(group.Key, group, current))
            .ToArray();
    }

    private SkillGroupViewModel CreateGroup(
        TechnologyCategory category,
        IEnumerable<Skill> skills,
        Language current)
    {
        var (icon, accent) = DescribeCategory(category);

        return new SkillGroupViewModel(
            Id: category.ToString().ToLowerInvariant(),
            Title: localizer[$"SkillGroup.{category}.Title"],
            Description: localizer[$"SkillGroup.{category}.Description"],
            Icon: icon,
            Accent: accent,
            Skills: skills.Select(skill => CreateItem(skill, current)).ToArray());
    }

    private SkillItemViewModel CreateItem(Skill skill, Language current)
    {
        var (percentage, cssClass) = skill.Level switch
        {
            SkillLevel.Expert => (92, "pf-level--expert"),
            SkillLevel.Advanced => (76, "pf-level--advanced"),
            _ => (58, "pf-level--intermediate"),
        };

        return new SkillItemViewModel(
            skill.Name.For(current),
            skill.Icon,
            localizer[$"SkillLevel.{skill.Level}"],
            percentage,
            cssClass);
    }

    private static (string Icon, string Accent) DescribeCategory(TechnologyCategory category) =>
        category switch
        {
            TechnologyCategory.Backend => ("server", "#58A6FF"),
            TechnologyCategory.Frontend => ("layout", "#79C0FF"),
            TechnologyCategory.Mobile => ("smartphone", "#A5D6FF"),
            TechnologyCategory.Cloud => ("cloud", "#6CB6FF"),
            _ => ("layers", "#388BFD"),
        };
}
