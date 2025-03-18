namespace Movieminds.Domain.Repositories;

public class Factory : IFactory
{
	private IEnumerable<Type> Constructs { get; set; }

	public Factory(params Type[] types)
	{
		Constructs = new List<Type>(types);
	}

	public T? Create<T>(params object?[]? parameters)
	{
		var type = typeof(T);

		if (!Constructs.Contains(type)) return Cast<T>(null);
		return Cast<T>(Activator.CreateInstance(type, parameters));
	}

	public static T? Cast<T>(object? obj)
	{
		return (T?)obj;
	}
}
