namespace Movieminds.Application.Commands.Messages;

public sealed record RemoveMessageCommand(int MessageId) : ICommand;
