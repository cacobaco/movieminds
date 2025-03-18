using Microsoft.Extensions.Options;
using Movieminds.Infrastructure.Contracts;

namespace Movieminds.Server.Configurations;

public class TmdbConfiguration : IConfigureOptions<TmdbOptions>
{
    private const string SectionName = "Tmdb";
    private readonly IConfiguration _configuration;

    public TmdbConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(TmdbOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
