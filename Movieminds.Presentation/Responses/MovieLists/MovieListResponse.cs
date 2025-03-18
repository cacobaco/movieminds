using Movieminds.Presentation.Responses.Movies;

namespace Movieminds.Presentation.Responses.MovieLists;

public sealed record MovieListResponse(
    int Id,
    IEnumerable<MovieResponse> Movies
);
