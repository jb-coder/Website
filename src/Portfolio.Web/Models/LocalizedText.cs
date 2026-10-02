namespace Portfolio.Web.Models;

/// <summary>
/// A piece of domain content available in both supported languages. Handlers
/// and factories resolve it, so view models and components always receive a
/// plain string.
/// </summary>
public sealed record LocalizedText(string Es, string En)
{
    public string For(Language language) =>
        language == Language.En ? En : Es;
}
