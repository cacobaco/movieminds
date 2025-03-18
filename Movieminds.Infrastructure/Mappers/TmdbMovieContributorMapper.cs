using Movieminds.Application.Contracts;
using Movieminds.Domain.Entities;
using Newtonsoft.Json.Linq;

namespace Movieminds.Infrastructure.Mappers;

public class TmdbMovieContributorMapper : IJsonMapper<MovieContributor>
{
    public MovieContributor Map(JObject json)
    {
        return new MovieContributor
        {
            Name = json["name"].Value<string>(),
            Role = json["known_for_department"].Value<string>(),
            ImageUrl = json["profile_path"]?.Value<string>(), // TODO review
        };
    }

    public IEnumerable<MovieContributor> Map(JArray jsonArray)
    {
        return jsonArray.Select((token) => Map(token.Value<JObject>()));
    }
}
