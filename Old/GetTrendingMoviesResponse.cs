using Movieminds.Domain.Entities;

namespace Movieminds.Application.Queries.Movies;

public sealed record GetTrendingMoviesResponse(IEnumerable<Movie> Movies);
