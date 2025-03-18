using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.WishLists;

public sealed record GetProfileWishListQuery(
    int ProfileId
) : IQuery<IResponse<GetWishListResponse>>;
