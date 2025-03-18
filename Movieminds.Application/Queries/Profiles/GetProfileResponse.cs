namespace Movieminds.Application.Queries.Profiles;

public sealed record GetProfileResponse(
    int Id,
    string Name,
    string Username,
    string AvatarImageUrl,
    string BannerImageUrl,
    bool IsPrivate
);
