using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Movieminds.Infrastructure.Contracts;

namespace Movieminds.Infrastructure.Extensions;

public class TmdbHttpClient : MovieHttpClient
{
    public TmdbHttpClient(IOptions<TmdbOptions> tmdbOptions) : base(tmdbOptions.Value.BaseUrl)
    {
        DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tmdbOptions.Value.AuthToken);
    }
}
