namespace Movieminds.Application.Commands.Posts;

public sealed record RemovePostCommand(int PostId) : ICommand;
