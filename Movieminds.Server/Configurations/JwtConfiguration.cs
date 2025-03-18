using Microsoft.Extensions.Options;
using Movieminds.Infrastructure.Authentication;

namespace Movieminds.Server.Configurations;

public class JwtConfiguration : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "Jwt";
    private readonly IConfiguration _configuration;

    public JwtConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
