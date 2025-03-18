using Movieminds.Application.Queries.Movies;

namespace Movieminds.Application.Queries.SeenLists;

public sealed record GetSeenListResponse(
    int Id,
    IEnumerable<GetMovieResponse> Movies
);
