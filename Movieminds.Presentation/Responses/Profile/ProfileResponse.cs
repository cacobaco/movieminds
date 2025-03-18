namespace Movieminds.Presentation.Responses.Profile;

public sealed record ProfileResponse(
    int Id,
    string Name,
    string Username,
    string AvatarImageUrl,
    string BannerImageUrl,
    bool IsPrivate
);
