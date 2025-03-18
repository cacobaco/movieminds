using System.Net.Http.Json;
using Movieminds.Presentation.Requests.Authentication;
using Movieminds.Presentation.Responses;
using Movieminds.Presentation.Responses.Authentication;

namespace Movieminds.Client.Services;

public class AuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Response<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("authentication/login", request);

        var loginResponse = await response.Content.ReadFromJsonAsync<Response<LoginResponse>>();

        if (loginResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the login response.");
        }

        return loginResponse;
    }

    public async Task<Response<RegisterResponse>> RegisterAsync(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("authentication/register", request);

        var registerResponse = await response.Content.ReadFromJsonAsync<Response<RegisterResponse>>();

        if (registerResponse is null)
        {
            throw new InvalidOperationException("Failed to deserialize the login response.");
        }

        return registerResponse;
    }
}
