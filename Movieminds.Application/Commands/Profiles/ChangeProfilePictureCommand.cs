namespace Movieminds.Application.Commands.Profiles;

public sealed record ChangeProfilePictureCommand(
	int ProfileId,
	string ImageUrl
) : ICommand;
