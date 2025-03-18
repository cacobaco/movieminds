using Movieminds.Application.Queries.Movies;
using Movieminds.Application.Requests;
using Movieminds.Infrastructure.Extensions;
using Movieminds.Infrastructure.Mappers;
using Newtonsoft.Json;

namespace Movieminds.Infrastructure.Queries.Movies;

public class GetTmdbMovieQueryHandler : GetExternalMovieQueryHandler
{
    private readonly TmdbMovieResponseMapper _mapper;

    public GetTmdbMovieQueryHandler(TmdbHttpClient httpClient, TmdbMovieResponseMapper mapper) : base(httpClient)
    {
        _mapper = mapper;
    }

    public override async Task<IResponse<GetMovieResponse>> HandleAsync(GetMovieQuery request)
    {
        var tmdbResponse = await HttpClient.GetAsync($"movie/{request.Id}?language=pt-PT");

        var content = tmdbResponse.Content;
        if (!tmdbResponse.IsSuccessStatusCode || content is null)
        {
            return Response.Fail<GetMovieResponse>("Movie not found");
        }

        var data = await content.ReadAsStringAsync();
        if (data is null)
        {
            return Response.Fail<GetMovieResponse>("Movie not found");
        }

        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(data);

        GetMovieResponse response = _mapper.Map(jsonResponse);
        return Response.Ok(response);
    }
}
