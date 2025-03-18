namespace Movieminds.Application.Requests;

public interface IRequestHandler<TRequest> where TRequest : IRequest<IResponse>
{
    Task<IResponse> HandleAsync(TRequest request);
}

public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : IResponse
{
    Task<TResponse> HandleAsync(TRequest request);
}
