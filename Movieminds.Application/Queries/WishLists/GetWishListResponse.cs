using Movieminds.Application.Queries.Movies;

namespace Movieminds.Application.Queries.WishLists;

public sealed record GetWishListResponse(
    int Id,
    IEnumerable<GetMovieResponse> Movies
);
