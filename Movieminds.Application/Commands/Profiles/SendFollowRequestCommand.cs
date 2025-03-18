namespace Movieminds.Application.Commands.Profiles;

public sealed record SendFollowRequestCommand(
	int SenderProfileId,
	int ReceiverProfileId
) : ICommand;
