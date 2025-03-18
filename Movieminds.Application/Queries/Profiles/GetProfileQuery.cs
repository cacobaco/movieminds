using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Profiles;

public sealed record GetProfileQuery(
	int ProfileId
) : IQuery<IResponse<GetProfileResponse>>;
