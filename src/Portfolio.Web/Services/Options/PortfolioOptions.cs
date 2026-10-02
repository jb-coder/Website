using System.ComponentModel.DataAnnotations;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Options;

/// <summary>
/// Strongly typed configuration for the whole site. Every personal data point
/// shown on the pages is parameterizable from <c>appsettings.json</c> and
/// validated on startup (fail fast). Text fields are bilingual so the site can
/// render both Spanish (default) and English.
/// </summary>
public sealed class PortfolioOptions : IValidatableObject
{
    public const string SectionName = "Portfolio";

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Initials { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public LocalizedText Role { get; set; } = new(string.Empty, string.Empty);

    [Required(AllowEmptyStrings = false)]
    public LocalizedText Tagline { get; set; } = new(string.Empty, string.Empty);

    [Required(AllowEmptyStrings = false)]
    public LocalizedText Summary { get; set; } = new(string.Empty, string.Empty);

    [Required(AllowEmptyStrings = false)]
    public LocalizedText Location { get; set; } = new(string.Empty, string.Empty);

    [Required(AllowEmptyStrings = false)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [Url]
    public string LinkedInUrl { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [Url]
    public string GitHubUrl { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [Url]
    public string PublicBaseUrl { get; set; } = string.Empty;

    [Range(0, 60)]
    public int YearsOfExperience { get; set; }

    [Range(0, 1000)]
    public int CompletedProjects { get; set; }

    [Required(AllowEmptyStrings = false)]
    public LocalizedText Availability { get; set; } = new(string.Empty, string.Empty);

    public IReadOnlyList<LocalizedText> FocusAreas { get; set; } = [];

    /// <summary>
    /// Deployment sub-path used by GitHub Pages project sites, e.g. <c>/my-repo/</c>.
    /// Always normalized with a leading and trailing slash.
    /// </summary>
    public string BasePath { get; set; } = "/";

    /// <summary>
    /// Repository folder that contains the pattern documentation,
    /// used to build "read the docs" links on the Architecture page.
    /// </summary>
    public string DocumentationPath { get; set; } = "src/Portfolio.Web/Documentation";

    /// <summary>
    /// Guarantees a canonical <c>BasePath</c> shape: <c>/</c> or <c>/segment/</c>.
    /// </summary>
    public string NormalizedBasePath
    {
        get
        {
            if (string.IsNullOrWhiteSpace(BasePath) || BasePath == "/")
            {
                return "/";
            }

            var trimmed = BasePath.Trim();
            if (!trimmed.StartsWith('/'))
            {
                trimmed = "/" + trimmed;
            }

            return trimmed.EndsWith('/') ? trimmed : trimmed + "/";
        }
    }

    /// <summary>
    /// Absolute URL of a repository document, used by the Architecture page.
    /// </summary>
    public string BuildDocumentationUrl(string documentPath)
    {
        var repository = GitHubUrl.TrimEnd('/');
        var path = documentPath.TrimStart('/');
        return $"{repository}/blob/main/{DocumentationPath.Trim('/')}/{path}";
    }

    /// <summary>
    /// Data annotations do not recurse into complex properties, so the
    /// localized values are validated here to keep the fail-fast guarantee.
    /// </summary>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        foreach (var (name, value) in BilingualFields())
        {
            if (string.IsNullOrWhiteSpace(value.Es) || string.IsNullOrWhiteSpace(value.En))
            {
                yield return new ValidationResult(
                    $"{name} must be defined in both 'Es' and 'En'.",
                    [name]);
            }
        }

        if (FocusAreas.Count == 0)
        {
            yield return new ValidationResult(
                "At least one focus area must be defined.",
                [nameof(FocusAreas)]);
        }

        for (var index = 0; index < FocusAreas.Count; index++)
        {
            var area = FocusAreas[index];
            if (string.IsNullOrWhiteSpace(area.Es) || string.IsNullOrWhiteSpace(area.En))
            {
                yield return new ValidationResult(
                    $"{nameof(FocusAreas)}[{index}] must be defined in both 'Es' and 'En'.",
                    [nameof(FocusAreas)]);
            }
        }
    }

    private IEnumerable<(string Name, LocalizedText Value)> BilingualFields()
    {
        yield return (nameof(Role), Role);
        yield return (nameof(Tagline), Tagline);
        yield return (nameof(Summary), Summary);
        yield return (nameof(Location), Location);
        yield return (nameof(Availability), Availability);
    }
}
