using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse> where TResponse : IResponse { }
