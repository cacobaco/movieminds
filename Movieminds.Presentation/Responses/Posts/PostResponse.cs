using Movieminds.Presentation.Responses.Movies;
using Movieminds.Presentation.Responses.Profile;

namespace Movieminds.Presentation.Responses.Posts;

public sealed record PostResponse(
    int Id,
    ProfileResponse Author,
    MovieResponse Movie,
    string Content,
    int LikesCount,
    DateTime CreatedAt
);
