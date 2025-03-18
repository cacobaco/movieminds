namespace Movieminds.Application.Commands.Profiles;

public sealed record ToggleProfileVisibilityCommand(
		int ProfileId
) : ICommand;
