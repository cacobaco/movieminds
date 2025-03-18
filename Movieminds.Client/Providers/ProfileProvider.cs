using Movieminds.Client.Services;
using Movieminds.Presentation.Responses.Profile;

namespace Movieminds.Client.Providers;

public class ProfileProvider : Observable
{
    private readonly ProfileService _profileService;
    private ProfileResponse? _profile;
    public ProfileResponse? Profile
    {
        get => _profile;
        set
        {
            _profile = value;
            InvokeNotify();
        }
    }

    public ProfileProvider(ProfileService profileService, UserProvider userProvider)
    {
        _profileService = profileService;
        userProvider.Notify += OnUserProviderNotify;

        if (userProvider.UserId is not null)
        {
            OnUserProviderNotify(this, EventArgs.Empty);
        }
    }

    public async void OnUserProviderNotify(object sender, EventArgs e)
    {
        if (sender is not UserProvider userProvider || userProvider.UserId is null)
        {
            return;
        }

        var response = await _profileService.GetProfileAsync((int)userProvider.UserId);
        Profile = response.Data;
    }
}
