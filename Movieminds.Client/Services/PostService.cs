using System.Net.Http.Json;
using Movieminds.Presentation.Requests.Posts;
using Movieminds.Presentation.Responses;
using Movieminds.Presentation.Responses.Posts;

namespace Movieminds.Client.Services;

public class PostService
{
    private readonly HttpClient _httpClient;

    public PostService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Response<PostResponse>> PostPostAsync(PostRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("post", request);

        var postResponse = await response.Content.ReadFromJsonAsync<Response<PostResponse>>();

        if (postResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the post response.");
        }

        return postResponse;
    }

    public async Task<Response<IEnumerable<PostResponse>>> GetPostsAsync(int? profileId = null, int? movieId = null)
    {
        var requestUrl = "post";

        if (profileId.HasValue)
        {
            requestUrl += $"?profileId={profileId}";
        }

        if (movieId.HasValue)
        {
            requestUrl += profileId.HasValue ? $"&movieId={movieId}" : $"?movieId={movieId}";
        }

        var response = await _httpClient.GetAsync(requestUrl);

        var postResponse = await response.Content.ReadFromJsonAsync<Response<IEnumerable<PostResponse>>>();

        if (postResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the post response.");
        }

        return postResponse;
    }

    public async Task<Response<IEnumerable<PostResponse>>> GetPostsByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"post/{id}");

        var postResponse = await response.Content.ReadFromJsonAsync<Response<IEnumerable<PostResponse>>>();

        if (postResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the post response.");
        }

        return postResponse;
    }
}
