using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Posts;

public sealed record GetUserPostsQuery(
    int UserId
) : IQuery<IResponse<GetUserPostsResponse>>;
