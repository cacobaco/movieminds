using Movieminds.Domain.Entities;

namespace Movieminds.Application.Queries.Users;

public sealed record GetUsersByNameResponse(IEnumerable<User> Users);
