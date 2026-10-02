namespace Portfolio.Web.Services.Mediator;

/// <summary>
/// Marker interface for fire-and-forget notifications. Introduced now so the
/// site is ready for cross-cutting concerns (analytics, cache invalidation).
/// </summary>
public interface INotification
{
}

/// <summary>
/// Handles a notification. Multiple handlers per notification are supported.
/// </summary>
public interface INotificationHandler<in TNotification>
    where TNotification : INotification
{
    Task HandleAsync(TNotification notification, CancellationToken cancellationToken = default);
}
