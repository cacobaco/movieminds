namespace Movieminds.Client.Providers;

public interface IObservable
{
    event EventHandler? Notify;
}
