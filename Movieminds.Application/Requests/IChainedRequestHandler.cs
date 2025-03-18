namespace Movieminds.Application.Requests;

public interface IChainedRequestHandler<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest<IResponse>
{
    IRequestHandler<TRequest> SetNextHandler(IRequestHandler<TRequest> nextHandler);
}

public interface IChainedRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : IResponse
{
    IRequestHandler<TRequest, TResponse> SetNextHandler(IRequestHandler<TRequest, TResponse> nextHandler);
}
