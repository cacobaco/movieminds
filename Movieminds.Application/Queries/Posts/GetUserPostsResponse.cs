using Movieminds.Domain.Entities;

namespace Movieminds.Application.Queries.Posts;

public sealed record GetUserPostsResponse(IEnumerable<Post> Posts);
