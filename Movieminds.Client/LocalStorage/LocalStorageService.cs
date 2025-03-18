using System.Text.Json;
using Microsoft.JSInterop;

namespace Movieminds.Client.LocalStorage;

public class LocalStorageService : ILocalStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SetItemAsync<T>(string key, T value)
    {
        var serializedValue = JsonSerializer.Serialize(value);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, serializedValue);
    }

    public async Task<T?> GetItemAsync<T>(string key)
    {
        var serializedValue = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        return serializedValue == null ? default : JsonSerializer.Deserialize<T>(serializedValue);
    }

    public async Task RemoveItemAsync(string key)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }
}
