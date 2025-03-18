using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.SeenLists;

public sealed record GetMoviesSeenListQuery(
	int UserId
) : IQuery<IResponse<GetMoviesSeenListResponse>>;
