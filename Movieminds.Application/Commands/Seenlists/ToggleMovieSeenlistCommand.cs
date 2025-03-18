namespace Movieminds.Application.Commands.SeenLists;

public sealed record ToggleMovieSeenListCommand(
	int UserId,
	int MovieId
) : ICommand;
