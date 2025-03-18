using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Posts;

public sealed record GetPostsQuery(
    int? ProfileId = null,
    int? MovieId = null
) : IQuery<ICollectionResponse<GetPostResponse>>;
