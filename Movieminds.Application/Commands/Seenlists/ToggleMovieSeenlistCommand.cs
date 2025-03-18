namespace Movieminds.Application.Commands.SeenLists;

public sealed record ToggleMovieSeenListCommand(
	int ProfileId,
	int MovieId
) : ICommand;
