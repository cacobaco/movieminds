namespace Movieminds.Application.Commands.Authentication;

public sealed record LoginResponse(
    string Token,
    int UserId
);
