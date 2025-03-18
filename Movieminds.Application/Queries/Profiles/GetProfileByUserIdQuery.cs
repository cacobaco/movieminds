using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Profiles;

public sealed record GetProfileByUserIdQuery(
	int UserId
) : IQuery<IResponse<GetProfileByUserIdResponse>>;
