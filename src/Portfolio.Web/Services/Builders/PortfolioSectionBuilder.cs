using Portfolio.Web.Models;
using Portfolio.Web.Services.Abstractions;

namespace Portfolio.Web.Services.Builders;

/// <summary>
/// Fluent builder for <see cref="PortfolioSection"/>. Registered as transient
/// so every consumer composes its own independent section.
/// </summary>
public sealed class PortfolioSectionBuilder : IPortfolioSectionBuilder
{
    private string _id = string.Empty;
    private string? _eyebrow;
    private string _title = string.Empty;
    private string? _subtitle;
    private string _cssClass = string.Empty;

    public IPortfolioSectionBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public IPortfolioSectionBuilder WithEyebrow(string eyebrow)
    {
        _eyebrow = eyebrow;
        return this;
    }

    public IPortfolioSectionBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public IPortfolioSectionBuilder WithSubtitle(string subtitle)
    {
        _subtitle = subtitle;
        return this;
    }

    public IPortfolioSectionBuilder WithCssClass(string cssClass)
    {
        _cssClass = cssClass;
        return this;
    }

    public PortfolioSection Build()
    {
        if (string.IsNullOrWhiteSpace(_id))
        {
            throw new InvalidOperationException("A section requires an identifier.");
        }

        if (string.IsNullOrWhiteSpace(_title))
        {
            throw new InvalidOperationException($"Section '{_id}' requires a title.");
        }

        return new PortfolioSection(_id, _eyebrow, _title, _subtitle, _cssClass);
    }
}
