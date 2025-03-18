namespace Movieminds.Application.Requests;

public interface IRequest<out TResponse> where TResponse : IResponse { }
