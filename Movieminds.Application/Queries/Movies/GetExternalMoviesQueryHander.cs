using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Movies;

public abstract class GetExternalMoviesQueryHandler : ChainedRequestHandler<GetMoviesQuery, IPaginatedResponse<GetMovieResponse>>
{
    public HttpClient HttpClient { get; set; }

    public GetExternalMoviesQueryHandler(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public override async Task<IPaginatedResponse<GetMovieResponse>> HandleAsync(GetMoviesQuery request)
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
