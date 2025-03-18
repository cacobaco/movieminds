using System.Net.Http.Json;
using Movieminds.Presentation.Requests.MovieLists;
using Movieminds.Presentation.Responses;
using Movieminds.Presentation.Responses.MovieLists;

namespace Movieminds.Client.Services;

public class MovieListService
{
    private readonly HttpClient _httpClient;

    public MovieListService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Response<MovieListResponse>> GetProfileMovieListAsync(string movieList, int profileId)
    {
        var response = await _httpClient.GetAsync($"{movieList}/profile/{profileId}");

        var movieListResponse = await response.Content.ReadFromJsonAsync<Response<MovieListResponse>>();
        if (movieListResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the movie list response.");
        }

        return movieListResponse;
    }

    public async Task<Response> ToggleMovieMovieListAsync(string movieList, int movieId)
    {
        var request = new ToggleMovieMovieListRequest
        {
            MovieId = movieId,
        };

        var response = await _httpClient.PostAsJsonAsync(movieList, request);

        var toggleResponse = await response.Content.ReadFromJsonAsync<Response>();
        if (toggleResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the toggle movie response.");
        }

        return toggleResponse;
    }
}