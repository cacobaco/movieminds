using Movieminds.Application.Queries.Movies;

namespace Movieminds.Application.Commands.Movies;

public sealed record SaveExternalMovieCommand(GetMovieResponse Response) : ICommand;
