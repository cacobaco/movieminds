using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.SeenLists;

public sealed record GetProfileSeenListQuery(
    int ProfileId
) : IQuery<IResponse<GetSeenListResponse>>;
