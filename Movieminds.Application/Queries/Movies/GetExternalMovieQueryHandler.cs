using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Movies;

public abstract class GetExternalMovieQueryHandler : ChainedRequestHandler<GetMovieQuery, IResponse<GetMovieResponse>>
{
    public HttpClient HttpClient { get; set; }

    public GetExternalMovieQueryHandler(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public override async Task<IResponse<GetMovieResponse>> HandleAsync(GetMovieQuery request)
    {
        try
        {
            return await base.HandleAsync(request);
        }
        catch (NotImplementedException)
        {
            return Response.Fail<GetMovieResponse>("Movie not found");
        }
    }
}
