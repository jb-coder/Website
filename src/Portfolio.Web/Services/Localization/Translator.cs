using System.Globalization;
using System.Resources;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Localization;

/// <summary>
/// Resource-manager based translator. Both language catalogues are embedded in
/// the main assembly under explicit manifest names, and the current URL
/// language decides which one is read.
/// </summary>
public sealed class Translator(ILanguageContext language) : ITranslator
{
    private const string NeutralResourceName = "Portfolio.Web.Resources.SharedResource";
    private const string EnglishResourceName = "Portfolio.Web.Resources.SharedResource.en";

    private static readonly ResourceManager Spanish =
        new(NeutralResourceName, typeof(SharedResource).Assembly);

    private static readonly ResourceManager English =
        new(EnglishResourceName, typeof(SharedResource).Assembly);

    public string this[string key] =>
        GetResourceManager().GetString(key, CultureInfo.InvariantCulture) ?? key;

    public string this[string key, params object[] arguments] =>
        string.Format(CultureInfo.InvariantCulture, this[key], arguments);

    private ResourceManager GetResourceManager() =>
        language.Language == Language.En ? English : Spanish;
}
