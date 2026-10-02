namespace Portfolio.Web.Services.Mediator;

/// <summary>
/// Handles a single <typeparamref name="TRequest"/> type. Registering handlers
/// in DI is all that is needed for the mediator to discover them.
/// </summary>
public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}
