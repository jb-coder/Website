namespace Portfolio.Web.Models;

/// <summary>
/// A design pattern applied in this solution, documented for the Architecture page.
/// </summary>
public sealed record ArchitecturePattern(
    string Id,
    string Name,
    string Intent,
    string Implementation,
    string Benefit,
    string Icon,
    string DocumentPath);
