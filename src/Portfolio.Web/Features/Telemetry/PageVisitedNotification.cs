using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Telemetry;

/// <summary>
/// Published when a page is rendered. Today it only logs; tomorrow it can feed
/// analytics without touching the pages (Open/Closed Principle).
/// </summary>
public sealed record PageVisitedNotification(string PageName) : INotification;
