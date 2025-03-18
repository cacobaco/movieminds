namespace Movieminds.Application.Commands.Posts;

public sealed record ToggleLikeCommand(
	int UserId,
	int PostId
) : ICommand;
