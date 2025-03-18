namespace Movieminds.Client.Providers;

public class Observable : IObservable
{
    public event EventHandler? Notify;

    public void InvokeNotify()
    {
        Notify?.Invoke(this, EventArgs.Empty);
    }
}
