namespace Movieminds.Infrastructure.Extensions;

public class MovieHttpClient : HttpClient
{
    public MovieHttpClient(string baseUrl)
    {
        BaseAddress = new Uri(baseUrl);
    }
}
