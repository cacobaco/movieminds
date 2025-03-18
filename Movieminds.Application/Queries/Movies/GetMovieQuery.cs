using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Movies;

public sealed record GetMovieQuery(int Id) : IQuery<IResponse<GetMovieResponse>>;
