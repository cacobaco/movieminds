using Movieminds.Domain.Entities;

namespace Movieminds.Application.Queries.SeenLists;

public sealed record GetMoviesSeenListResponse(IEnumerable<Movie> Movies);
