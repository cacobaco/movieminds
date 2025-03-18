namespace Movieminds.Application.Commands.Profiles;

public sealed record RejectFollowRequestCommand(
	int SenderProfileId,
	int ReceiverProfileId
) : ICommand;
