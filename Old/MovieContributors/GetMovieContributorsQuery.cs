using Movieminds.Application.Requests;

namespace Movieminds.Application.Queries.MovieContributors;

public sealed record GetMovieContributorsQuery(int MovieId) : IQuery<IResponse<GetMovieContributorsResponse>>;
