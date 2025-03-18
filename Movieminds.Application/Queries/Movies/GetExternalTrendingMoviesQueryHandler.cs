using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Movies;

public abstract class GetExternalTrendingMoviesQueryHandler : ChainedRequestHandler<GetTrendingMoviesQuery, IPaginatedResponse<GetMovieResponse>>
{
    public HttpClient HttpClient { get; set; }

    public GetExternalTrendingMoviesQueryHandler(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public override async Task<IPaginatedResponse<GetMovieResponse>> HandleAsync(GetTrendingMoviesQuery request)
    {
        try
        {
            return await base.HandleAsync(request);
        }
        catch (NotImplementedException)
        {
            return PaginatedResponse<GetMovieResponse>.Fail();
        }
    }
}
