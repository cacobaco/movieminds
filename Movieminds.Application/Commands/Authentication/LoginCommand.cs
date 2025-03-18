using Movieminds.Application.Requests;

namespace Movieminds.Application.Commands.Authentication;

public sealed record LoginCommand(
    string Identifier,
    string Password
) : ICommand<IResponse<LoginResponse>>;
