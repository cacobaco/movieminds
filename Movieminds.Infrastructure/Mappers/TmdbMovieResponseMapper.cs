using Microsoft.Extensions.Options;
using Movieminds.Application.Contracts;
using Movieminds.Application.Queries.Movies;
using Movieminds.Infrastructure.Contracts;
using Newtonsoft.Json.Linq;

namespace Movieminds.Infrastructure.Mappers;

public class TmdbMovieResponseMapper : IJsonMapper<GetMovieResponse>
{
    private readonly string _posterImageBaseUrl;

    public TmdbMovieResponseMapper(IOptions<TmdbOptions> tmdbOptions)
    {
        _posterImageBaseUrl = tmdbOptions.Value.PosterImageBaseUrl;
    }

    public GetMovieResponse Map(JObject json)
    {
        var id = json.ContainsKey("id") ? json["id"].Value<int>() : 1;

        var title = json.ContainsKey("title") ? json["title"].Value<string>() : "Dummy Title";
        title = string.IsNullOrWhiteSpace(title) ? "Dummy Title" : title;

        var overview = json.ContainsKey("overview") ? json["overview"].Value<string>() : "Dummy Overview";
        overview = string.IsNullOrWhiteSpace(overview) ? "Dummy Overview" : overview;

        var posterPath = json.ContainsKey("poster_path") ? json["poster_path"].Value<string>() : null;
        posterPath = string.IsNullOrWhiteSpace(posterPath) ? "/img/default-movie-poster.jpg" : $"{_posterImageBaseUrl}{posterPath}";

        var voteAverage = json.ContainsKey("vote_average") ? json["vote_average"].Value<double>() : 0.0;

        var releaseDateString = json.ContainsKey("release_date") ? json["release_date"].Value<string?>() : null;
        releaseDateString = string.IsNullOrWhiteSpace(releaseDateString) ? null : releaseDateString.Split(" ")[0];
        var releaseDate = DateOnly.TryParse(releaseDateString, out DateOnly date) ? date : (DateOnly?)null;

        return new GetMovieResponse(
            id,
            title,
            overview,
            posterPath,
            voteAverage,
            releaseDate
        );
    }
    public IEnumerable<GetMovieResponse> Map(JArray jsonArray)
    {
        return jsonArray.Select((token) => Map(token.Value<JObject>()));
    }
}
