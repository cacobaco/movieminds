namespace Movieminds.Application.Commands.Profiles;

public sealed record ChangeBannerPictureCommand(
	int ProfileId,
	string BannerImageUrl
) : ICommand;
