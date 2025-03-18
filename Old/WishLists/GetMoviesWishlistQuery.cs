using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.WishLists;

public sealed record GetMoviesWishListQuery(
	int UserId
) : IQuery<IResponse<GetMoviesWishListResponse>>;
