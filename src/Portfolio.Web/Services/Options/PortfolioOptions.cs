using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.Services.Options;

/// <summary>
/// Strongly typed configuration for the whole site. Every personal data point
/// shown on the pages is parameterizable from <c>appsettings.json</c> and
/// validated on startup (fail fast).
/// </summary>
public sealed class PortfolioOptions
{
    public const string SectionName = "Portfolio";

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Initials { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Role { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Tagline { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Summary { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Location { get; set; } = string.Empty;

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
    public string Availability { get; set; } = string.Empty;

    /// <summary>
    /// Deployment sub-path used by GitHub Pages project sites, e.g. <c>/my-repo/</c>.
    /// Always normalized with a leading and trailing slash.
    /// </summary>
    public string BasePath { get; set; } = "/";

    public IReadOnlyList<string> FocusAreas { get; set; } = [];

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
}
