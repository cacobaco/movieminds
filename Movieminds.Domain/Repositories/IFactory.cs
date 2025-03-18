namespace Movieminds.Domain.Repositories;

public interface IFactory
{
	T? Create<T>(params object?[]? parameters);
}
