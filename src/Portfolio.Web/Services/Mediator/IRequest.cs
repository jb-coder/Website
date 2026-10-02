namespace Portfolio.Web.Services.Mediator;

/// <summary>
/// Marker interface for a request that produces a <typeparamref name="TResponse"/>.
/// </summary>
public interface IRequest<out TResponse>
{
}
