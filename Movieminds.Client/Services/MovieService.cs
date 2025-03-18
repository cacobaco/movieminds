using System.Net.Http.Json;
using Movieminds.Presentation.Responses;
using Movieminds.Presentation.Responses.Movies;

namespace Movieminds.Client.Services;

public class MovieService
{
    private readonly HttpClient _httpClient;

    public MovieService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaginatedResponse<MovieResponse>> GetMoviesAsync(int page = 1, string sortBy = "popularity.desc", string search = "")
    {
        var requestUrl = $"movie?pageNumber={page}";

        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            requestUrl += $"&sortBy={sortBy}";
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            requestUrl += $"&search={search}";
        }

        var response = await _httpClient.GetAsync(requestUrl);

        var moviesResponse = await response.Content.ReadFromJsonAsync<PaginatedResponse<MovieResponse>>();
        if (moviesResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the movie response.");
        }

        return moviesResponse;
    }

    public async Task<PaginatedResponse<MovieResponse>> GetTrendingMoviesAsync(int page = 1)
    {
        var response = await _httpClient.GetAsync($"movie/trending?pageNumber={page}");

        var moviesResponse = await response.Content.ReadFromJsonAsync<PaginatedResponse<MovieResponse>>();
        if (moviesResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the movie response.");
        }

        return moviesResponse;
    }

    public async Task<Response<MovieResponse>> GetMovieAsync(int id)
    {
        var response = await _httpClient.GetAsync($"movie/{id}");

        var movieResponse = await response.Content.ReadFromJsonAsync<Response<MovieResponse>>();
        if (movieResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the movie response.");
        }

        return movieResponse;
    }
}
