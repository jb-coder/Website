using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Portfolio.Web.Services.Mediator;

/// <summary>
/// Reflection-based mediator that resolves the matching
/// <see cref="IRequestHandler{TRequest,TResponse}"/> from the DI container.
/// One handler per request keeps the flow explicit and testable.
/// </summary>
public sealed class PortfolioMediator(IServiceProvider serviceProvider) : IMediator
{
    private const string HandleMethodName = nameof(IRequestHandler<IRequest<object>, object>.HandleAsync);

    public Task<TResponse> SendAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

        var handler = serviceProvider.GetService(handlerType)
            ?? throw new InvalidOperationException(
                $"No IRequestHandler was registered for request '{requestType.Name}'.");

        var handleMethod = handlerType.GetMethod(HandleMethodName)
            ?? throw new InvalidOperationException(
                $"Handler '{handlerType.Name}' does not expose '{HandleMethodName}'.");

        try
        {
            var result = handleMethod.Invoke(handler, [request, cancellationToken]);
            return (Task<TResponse>)result!;
        }
        catch (TargetInvocationException exception) when (exception.InnerException is not null)
        {
            ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
            throw;
        }
    }

    public async Task PublishAsync<TNotification>(
        TNotification notification,
        CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        ArgumentNullException.ThrowIfNull(notification);

        var handlers = serviceProvider.GetServices<INotificationHandler<TNotification>>();
        foreach (var handler in handlers)
        {
            await handler.HandleAsync(notification, cancellationToken).ConfigureAwait(false);
        }
    }
}
