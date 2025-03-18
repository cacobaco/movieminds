using Movieminds.Application.Queries.Movies;

namespace Movieminds.Application.Commands.Movies;

public sealed record SaveExternalMoviesCommand(IEnumerable<GetMovieResponse> Response) : ICommand;
