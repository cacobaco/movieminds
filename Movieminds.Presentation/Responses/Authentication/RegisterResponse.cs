namespace Movieminds.Presentation.Responses.Authentication;

public sealed record RegisterResponse(
    string Token,
    int UserId
);
