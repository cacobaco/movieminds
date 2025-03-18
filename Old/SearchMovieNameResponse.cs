using Movieminds.Domain.Entities;

namespace Movieminds.Application.Queries.Movies;

public sealed record SearchMovieNameResponse(IEnumerable<Movie> Movies);
