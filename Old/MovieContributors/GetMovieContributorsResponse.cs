using Movieminds.Domain.Entities;

namespace Movieminds.Application.Queries.MovieContributors;

public sealed record GetMovieContributorsResponse(IEnumerable<MovieContributor> MovieContributors);
