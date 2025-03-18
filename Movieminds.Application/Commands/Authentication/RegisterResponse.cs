namespace Movieminds.Application.Commands.Authentication;

public sealed record RegisterResponse(
    string Token,
    int UserId
);
