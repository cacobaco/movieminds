using System.Net.Http.Json;
using Movieminds.Presentation.Responses;
using Movieminds.Presentation.Responses.SeenList;

namespace Movieminds.Client.Services;

public class SeenListService
{
    private readonly HttpClient _httpClient;

    public SeenListService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Response<SeenListResponse>> GetSeenListAsync(int MovieId)
    {
        var response = await _httpClient.GetAsync($"seenlist/{MovieId}");

        var seenListResponse = await response.Content.ReadFromJsonAsync<Response<SeenListResponse>>();

        if (seenListResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the seenlist response.");
        }

        return seenListResponse;
    }

    public async Task<Response<SeenListResponse>> ToogleSeenListAsync(int MovieId)
    {
        var response = await _httpClient.PostAsJsonAsync("seenlist", MovieId);

        var seenListResponse = await response.Content.ReadFromJsonAsync<Response<SeenListResponse>>();

        if (seenListResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the wishlist response.");
        }

        return seenListResponse;
    }
}