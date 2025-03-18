using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Movies;

public sealed record GetTrendingMoviesQuery() : IQuery<IResponse<GetTrendingMoviesResponse>>;
