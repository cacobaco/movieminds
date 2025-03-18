using System.Net.Http.Json;
using Movieminds.Presentation.Responses;
using Movieminds.Presentation.Responses.Profile;

namespace Movieminds.Client.Services;

public class ProfileService
{
    private readonly HttpClient _httpClient;

    public ProfileService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Response<ProfileResponse>> GetProfileAsync(int id)
    {
        var response = await _httpClient.GetAsync($"profile/{id}");

        var profileResponse = await response.Content.ReadFromJsonAsync<Response<ProfileResponse>>();

        if (profileResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the profile response.");
        }

        return profileResponse;
    }
}
