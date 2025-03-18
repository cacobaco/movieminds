namespace Movieminds.Application.Commands.Messages;

public sealed record MarkMessagesAsSeenCommand(
    int SenderId,
    int ReceiverId
) : ICommand;
