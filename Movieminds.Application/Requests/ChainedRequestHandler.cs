namespace Movieminds.Application.Requests;

public abstract class ChainedRequestHandler<TRequest> : IChainedRequestHandler<TRequest> where TRequest : IRequest<IResponse>
{
    private IRequestHandler<TRequest>? _nextHandler;

    public IRequestHandler<TRequest> SetNextHandler(IRequestHandler<TRequest> nextHandler)
    {
        _nextHandler = nextHandler;
        return _nextHandler;
    }

    public virtual async Task<IResponse> HandleAsync(TRequest request)
    {
        if (_nextHandler != null)
        {
            return await _nextHandler.HandleAsync(request);
        }

        return Response.Ok();
    }
}

public abstract class ChainedRequestHandler<TRequest, TResponse> : IChainedRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : IResponse
{
    private IRequestHandler<TRequest, TResponse>? _nextHandler;

    public IRequestHandler<TRequest, TResponse> SetNextHandler(IRequestHandler<TRequest, TResponse> nextHandler)
    {
        _nextHandler = nextHandler;
        return _nextHandler;
    }

    public virtual async Task<TResponse> HandleAsync(TRequest request)
    {
        if (_nextHandler != null)
        {
            return await _nextHandler.HandleAsync(request);
        }
        throw new NotImplementedException();
    }
}
