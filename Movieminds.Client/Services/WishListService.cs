using System.Net.Http.Json;
using Movieminds.Presentation.Responses;
using Movieminds.Presentation.Responses.WishList;

namespace Movieminds.Client.Services;

public class WishListService
{
    private readonly HttpClient _httpClient;

    public WishListService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Response<WishListResponse>> GetSeenListAsync(int id)
    {
        var response = await _httpClient.GetAsync($"wishlist/{id}");

        var wishListResponse = await response.Content.ReadFromJsonAsync<Response<WishListResponse>>();

        if (wishListResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the wishlist response.");
        }

        return wishListResponse;
    }

    public async Task<Response<WishListResponse>> ToogleSeenListAsync(int id)
    {
        var response = await _httpClient.PostAsJsonAsync("wishlist", id);

        var wishListResponse = await response.Content.ReadFromJsonAsync<Response<WishListResponse>>();

        if (wishListResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the wishlist response.");
        }

        return wishListResponse;
    }
}