namespace Movieminds.Application.Commands.Posts;

public sealed record CreatePostCommand(
	int AuthorId,
	int MovieId,
	string Content
) : ICommand;
