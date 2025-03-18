namespace Movieminds.Presentation.Responses.Authentication;

public sealed record LoginResponse(
    string Token,
    int UserId
);
