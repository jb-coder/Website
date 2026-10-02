using Portfolio.Web.Services.Mediator;

namespace Portfolio.Web.Features.Telemetry;

/// <summary>
/// Notification handler that writes page visits to the application log.
/// </summary>
public sealed partial class PageVisitedLoggingHandler(ILogger<PageVisitedLoggingHandler> logger)
    : INotificationHandler<PageVisitedNotification>
{
    public Task HandleAsync(
        PageVisitedNotification notification,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(notification);
        LogPageVisited(logger, notification.PageName);

        return Task.CompletedTask;
    }
}
