namespace Portfolio.Web.Services.Localization;

/// <summary>
/// Resolves UI strings for the current <see cref="ILanguageContext"/>.
/// Indexers mirror the <c>IStringLocalizer</c> syntax while keeping the lookup
/// fully deterministic: the language comes from the URL, never from ambient
/// culture or satellite assemblies.
/// </summary>
public interface ITranslator
{
    string this[string key] { get; }

    string this[string key, params object[] arguments] { get; }
}
