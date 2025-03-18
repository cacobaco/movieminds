using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Movies;

public sealed record GetMoviesQuery(
    string Search = "",
    string SortBy = "popularity.desc",
    int PageNumber = 1,
    int PageSize = 20
) : IQuery<IPaginatedResponse<GetMovieResponse>>;
