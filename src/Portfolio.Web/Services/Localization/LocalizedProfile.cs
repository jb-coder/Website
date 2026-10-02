using Microsoft.Extensions.Options;
using Portfolio.Web.Models;
using Portfolio.Web.Services.Options;

namespace Portfolio.Web.Services.Localization;

/// <summary>
/// The profile configuration with every localized value already resolved for
/// the current request. Components consume this instead of repeating
/// <c>.For(language)</c> calls in markup.
/// </summary>
public sealed class LocalizedProfile(IOptions<PortfolioOptions> options, ILanguageContext language)
{
    private readonly PortfolioOptions _options = options.Value;

    private Language Current => language.Language;

    public string Name => _options.Name;

    public string Initials => _options.Initials;

    public string Role => _options.Role.For(Current);

    public string Tagline => _options.Tagline.For(Current);

    public string Summary => _options.Summary.For(Current);

    public string Location => _options.Location.For(Current);

    public string Email => _options.Email;

    public string LinkedInUrl => _options.LinkedInUrl;

    public string GitHubUrl => _options.GitHubUrl;

    public string Availability => _options.Availability.For(Current);

    public int YearsOfExperience => _options.YearsOfExperience;

    public int CompletedProjects => _options.CompletedProjects;

    public string PublicBaseUrl => _options.PublicBaseUrl;

    public string NormalizedBasePath => _options.NormalizedBasePath;

    public IReadOnlyList<string> FocusAreas =>
        _options.FocusAreas.Select(area => area.For(Current)).ToArray();
}
