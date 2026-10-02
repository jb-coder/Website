using System.Globalization;

namespace Portfolio.Web.Models;

/// <summary>
/// Timeline entry: a role, a degree or a certification.
/// </summary>
public sealed record Experience(
    string Id,
    string Role,
    string Company,
    string Location,
    DateOnly StartDate,
    DateOnly? EndDate,
    string Summary,
    ExperienceType Type,
    string Icon,
    IReadOnlyList<string> Highlights,
    IReadOnlyList<string> Technologies)
{
    /// <summary>
    /// Culture-invariant period label, e.g. <c>Mar 2021 — Present</c>.
    /// Kept invariant so static exports are deterministic.
    /// </summary>
    public string Period
    {
        get
        {
            var start = StartDate.ToString("MMM yyyy", CultureInfo.InvariantCulture);
            var end = EndDate is null
                ? "Present"
                : EndDate.Value.ToString("MMM yyyy", CultureInfo.InvariantCulture);
            return $"{start} — {end}";
        }
    }
}
