using Movieminds.Client.Authentication;

namespace Movieminds.Client.Providers;

public class UserProvider : Observable
{
    private int? _userId;
    public int? UserId
    {
        get => _userId;
        set
        {
            _userId = value;
            InvokeNotify();
        }
    }

    public UserProvider(JwtAuthenticationStateProvider jwtAuthenticationStateProvider)
    {
        jwtAuthenticationStateProvider.Notify += OnNotify;

        if (jwtAuthenticationStateProvider.UserId is not null)
        {
            OnNotify(jwtAuthenticationStateProvider, EventArgs.Empty);
        }
    }

    public void OnNotify(object sender, EventArgs e)
    {
        if (sender is not JwtAuthenticationStateProvider jwtAuthenticationStateProvider)
        {
            return;
        }

        UserId = jwtAuthenticationStateProvider.UserId;
    }
}
