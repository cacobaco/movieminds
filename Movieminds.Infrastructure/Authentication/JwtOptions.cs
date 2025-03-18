namespace Movieminds.Infrastructure.Authentication;

public class JwtOptions
{
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string SecretKey { get; init; }
    public int ExpiryInMinutes { get; init; }
}
