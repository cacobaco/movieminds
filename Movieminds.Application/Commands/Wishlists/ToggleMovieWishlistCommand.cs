namespace Movieminds.Application.Commands.WishLists;

public sealed record ToggleMovieWishListCommand(
	int ProfileId,
	int MovieId
) : ICommand;
