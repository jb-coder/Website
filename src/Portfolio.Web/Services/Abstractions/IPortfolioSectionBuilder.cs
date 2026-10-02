using Portfolio.Web.Models;

namespace Portfolio.Web.Services.Abstractions;

/// <summary>
/// Builder Pattern: composes the (otherwise repeated) section header model
/// with a fluent, readable API.
/// </summary>
public interface IPortfolioSectionBuilder
{
    IPortfolioSectionBuilder WithId(string id);

    IPortfolioSectionBuilder WithEyebrow(string eyebrow);

    IPortfolioSectionBuilder WithTitle(string title);

    IPortfolioSectionBuilder WithSubtitle(string subtitle);

    IPortfolioSectionBuilder WithCssClass(string cssClass);

    PortfolioSection Build();
}
