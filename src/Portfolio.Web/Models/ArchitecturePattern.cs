namespace Portfolio.Web.Models;

/// <summary>
/// A design pattern applied in this solution, documented for the Architecture
/// page. Text is bilingual.
/// </summary>
public sealed record ArchitecturePattern(
    string Id,
    string Name,
    LocalizedText Intent,
    LocalizedText Implementation,
    LocalizedText Benefit,
    string Icon,
    string DocumentPath);
