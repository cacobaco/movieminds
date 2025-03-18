using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Movies;

public sealed record GetTrendingMoviesQuery(
    string? SearchQuery,
    int PageNumber = 1,
    int PageSize = 20
) : IQuery<IPaginatedResponse<GetMovieResponse>>;
