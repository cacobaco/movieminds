using Movieminds.Domain.Entities;

namespace Movieminds.Application.Queries.WishLists;

public sealed record GetMoviesWishListResponse(IEnumerable<Movie> Movies);
