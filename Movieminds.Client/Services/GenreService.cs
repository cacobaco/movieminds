using System.Net.Http.Json;
using Movieminds.Presentation.Responses;
using Movieminds.Presentation.Responses.Genre;

namespace Movieminds.Client.Services;

public class GenreService
{
    private readonly HttpClient _httpClient;

    public GenreService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Response<GenreResponse>> GetGenreAsync(int id)
    {
        var response = await _httpClient.GetAsync($"genre/{id}");

        var genreResponse = await response.Content.ReadFromJsonAsync<Response<GenreResponse>>();

        if (genreResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the genre response.");
        }

        return genreResponse;
    }

    public async Task<Response<GenreResponse>> CreateGenreAsync(string name)
    {
        var response = await _httpClient.PostAsJsonAsync("genre", name);

        var genreResponse = await response.Content.ReadFromJsonAsync<Response<GenreResponse>>();

        if (genreResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the genre response.");
        }

        return genreResponse;
    }
}