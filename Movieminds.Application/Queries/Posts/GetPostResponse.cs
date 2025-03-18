using Movieminds.Application.Queries.Movies;
using Movieminds.Application.Queries.Profiles;

namespace Movieminds.Application.Queries.Posts;

public sealed record GetPostResponse(
    int Id,
    GetProfileResponse Author,
    GetMovieResponse Movie,
    string Content,
    int LikesCount,
    DateTime CreatedAt
);
