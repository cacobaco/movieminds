using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Movieminds.Client.LocalStorage;
using Movieminds.Client.Providers;

namespace Movieminds.Client.Authentication;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider, IObservable
{
    public event EventHandler? Notify;

    public string? Token { get; private set; }
    public int? UserId { get; private set; }

    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public JwtAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        Token = await _localStorage.GetItemAsync<string?>("token");
        UserId = await _localStorage.GetItemAsync<int?>("userId");
        if (string.IsNullOrEmpty(Token) || UserId is null || UserId <= 0)
        {
            await UpdateLocalStorage();
            _httpClient.DefaultRequestHeaders.Authorization = null;
            Notify?.Invoke(this, EventArgs.Empty);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var claims = ParseClaimsFromJwt(Token);
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        Notify?.Invoke(this, EventArgs.Empty);
        return new AuthenticationState(user);
    }

    public async Task MarkUserAsAuthenticatedAsync(string token, int userId)
    {
        Token = token;
        UserId = userId;
        await UpdateLocalStorage();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task MarkUserAsLoggedOutAsync()
    {
        Token = null;
        UserId = null;
        await UpdateLocalStorage();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        return keyValuePairs?.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString() ?? string.Empty)) ?? [];
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }

        return Convert.FromBase64String(base64);
    }

    private async Task UpdateLocalStorage()
    {
        if (string.IsNullOrEmpty(Token) || UserId is null || UserId <= 0)
        {
            Token = null;
            UserId = null;
            await _localStorage.RemoveItemAsync("token");
            await _localStorage.RemoveItemAsync("userId");
        }
        else
        {
            await _localStorage.SetItemAsync("token", Token);
            await _localStorage.SetItemAsync("userId", UserId);
        }
    }
}
