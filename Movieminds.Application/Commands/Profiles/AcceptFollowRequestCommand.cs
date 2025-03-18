namespace Movieminds.Application.Commands.Profiles;

public sealed record AcceptFollowRequestCommand(
	int SenderProfileId,
	int ReceiverProfileId
) : ICommand;
