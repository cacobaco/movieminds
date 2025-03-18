namespace Movieminds.Application.Requests;

public interface IRequestMediator
{
    Task<IResponse> SendAsync(IRequest<IResponse> request);
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request) where TResponse : IResponse;
}
