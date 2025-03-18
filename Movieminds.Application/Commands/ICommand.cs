using Movieminds.Application.Requests;

namespace Movieminds.Application.Commands;

public interface ICommand : IRequest<IResponse> { }

public interface ICommand<out TResponse> : IRequest<TResponse> where TResponse : IResponse { }
