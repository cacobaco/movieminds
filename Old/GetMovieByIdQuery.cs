using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.Movies;

public sealed record GetMovieByIdQuery(int Id) : IQuery<IResponse<GetMovieByIdResponse>>;
