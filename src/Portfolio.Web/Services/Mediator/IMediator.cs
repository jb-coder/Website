namespace Portfolio.Web.Services.Mediator;

/// <summary>
/// Lightweight in-process mediator. It decouples pages from the services that
/// fulfill their use cases without pulling an external dependency.
/// </summary>
public interface IMediator
{
    Task<TResponse> SendAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default);

    Task PublishAsync<TNotification>(
        TNotification notification,
        CancellationToken cancellationToken = default)
        where TNotification : INotification;
}
