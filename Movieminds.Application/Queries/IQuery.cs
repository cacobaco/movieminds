using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : IResponse { }
