using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Profiles;

public sealed record GetProfilesQuery() : IQuery<ICollectionResponse<GetProfileResponse>>;
