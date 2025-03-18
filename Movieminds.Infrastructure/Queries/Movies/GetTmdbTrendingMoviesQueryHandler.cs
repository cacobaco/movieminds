using Movieminds.Application.Queries.Movies;
using Movieminds.Application.Requests;
using Movieminds.Infrastructure.Extensions;
using Movieminds.Infrastructure.Mappers;
using Newtonsoft.Json;

namespace Movieminds.Infrastructure.Queries.Movies;

public class GetTmdbTrendingMoviesQueryHandler : GetExternalTrendingMoviesQueryHandler
{
    private readonly TmdbMovieResponseMapper _mapper;

    public GetTmdbTrendingMoviesQueryHandler(TmdbHttpClient httpClient, TmdbMovieResponseMapper mapper) : base(httpClient)
    {
        _mapper = mapper;
    }

    public override async Task<IPaginatedResponse<GetMovieResponse>> HandleAsync(GetTrendingMoviesQuery request)
    {
        var tmdbResponse = await HttpClient.GetAsync($"movie/popular?page={request.PageNumber}&language=pt-PT");

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
        return PaginatedResponse<GetMovieResponse>.Ok(response, request.PageNumber, request.PageSize);
    }
}
