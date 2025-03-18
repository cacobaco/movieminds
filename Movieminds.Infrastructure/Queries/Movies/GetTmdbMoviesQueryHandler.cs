using Movieminds.Application.Queries.Movies;
using Movieminds.Application.Requests;
using Movieminds.Infrastructure.Extensions;
using Movieminds.Infrastructure.Mappers;
using Newtonsoft.Json;

namespace Movieminds.Infrastructure.Queries.Movies;

public class GetTmdbMoviesQueryHandler : GetExternalMoviesQueryHandler
{
    private readonly TmdbMovieResponseMapper _mapper;

    public GetTmdbMoviesQueryHandler(TmdbHttpClient httpClient, TmdbMovieResponseMapper mapper) : base(httpClient)
    {
        _mapper = mapper;
    }

    public override async Task<IPaginatedResponse<GetMovieResponse>> HandleAsync(GetMoviesQuery request)
    {
        var requestUrl = string.IsNullOrEmpty(request.Search) ?
            $"discover/movie?language=pt-PT"
            : $"search/movie?query={request.Search}&language=pt-PT";

        requestUrl += $"&page={request.PageNumber}";

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            requestUrl += $"&sort_by={request.SortBy}";
        }

        var tmdbResponse = await HttpClient.GetAsync(requestUrl);

        var content = tmdbResponse.Content;
        if (!tmdbResponse.IsSuccessStatusCode || content is null)
        {
            return PaginatedResponse<GetMovieResponse>.Fail("Movie not found");
        }

        var data = await content.ReadAsStringAsync();
        if (data is null)
        {
            return PaginatedResponse<GetMovieResponse>.Fail("Movie not found");
        }

        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(data);

        IEnumerable<GetMovieResponse> response = _mapper.Map(jsonResponse.results);

        int totalPages = jsonResponse.total_pages;
        int totalResults = jsonResponse.total_results;

        Console.WriteLine($"Total pages: {totalPages}");
        Console.WriteLine($"Total results: {totalResults}");

        return PaginatedResponse<GetMovieResponse>.Ok(response, request.PageNumber, request.PageSize, (int)totalPages, (int)totalResults);
    }
}
