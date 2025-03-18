namespace Movieminds.Application.Requests;

public class RequestMediator : IRequestMediator
{
    private readonly IServiceProvider _serviceProvider;

    public RequestMediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<IResponse> SendAsync(IRequest<IResponse> request)
    {
        var requestType = request.GetType();

        var handlerType = typeof(IRequestHandler<>).MakeGenericType(requestType);

        var handler = _serviceProvider.GetService(handlerType);

        if (handler is null || !handler.GetType().IsAssignableTo(handlerType))
        {
            throw new InvalidOperationException($"Handler not found for request type: {request.GetType().Name}");
        }

        return await ((dynamic)handler).HandleAsync((dynamic)request);
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request) where TResponse : IResponse
    {
        var requestType = request.GetType();
        var responseType = typeof(TResponse);

        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, responseType);

        var handler = _serviceProvider.GetService(handlerType);

        if (handler is null || !handler.GetType().IsAssignableTo(handlerType))
        {
            throw new InvalidOperationException($"Handler not found for request type: {request.GetType().Name}");
        }

        return await ((dynamic)handler).HandleAsync((dynamic)request);
    }
}
