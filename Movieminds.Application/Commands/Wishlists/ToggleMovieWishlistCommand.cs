namespace Movieminds.Application.Commands.WishLists;

public sealed record ToggleMovieWishListCommand(
	int UserId,
	int MovieId
) : ICommand;
