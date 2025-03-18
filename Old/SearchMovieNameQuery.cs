using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Movies;

public sealed record SearchMovieNameQuery(
	string Name,
	int PageNumber
) : IQuery<IResponse<SearchMovieNameResponse>>;
