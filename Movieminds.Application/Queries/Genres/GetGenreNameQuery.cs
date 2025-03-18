using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Genres;

public sealed record GetGenreNameQuery(
	int GenreId
) : IQuery<IResponse<GetGenreNameResponse>>;
