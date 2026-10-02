using System.Globalization;

namespace Portfolio.Web.Models;

/// <summary>
/// Timeline entry: a role, a degree or a certification. Text is bilingual and
/// the date labels are culture-invariant so the static export is deterministic;
/// the "present" wording is resolved by the component from the resources.
/// </summary>
public sealed record Experience(
    string Id,
    LocalizedText Role,
    string Company,
    string Location,
    DateOnly StartDate,
    DateOnly? EndDate,
    LocalizedText Summary,
    ExperienceType Type,
    string Icon,
    IReadOnlyList<LocalizedText> Highlights,
    IReadOnlyList<string> Technologies)
{
    /// <summary>Culture-invariant start label, e.g. <c>Sep 2025</c>.</summary>
    public string StartLabel => StartDate.ToString("MMM yyyy", CultureInfo.InvariantCulture);

    /// <summary>
    /// Culture-invariant end label, or <c>null</c> when the role is current.
    /// </summary>
    public string? EndLabel => EndDate?.ToString("MMM yyyy", CultureInfo.InvariantCulture);
}
