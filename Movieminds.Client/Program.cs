using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Movieminds.Client.Authentication;
using Movieminds.Client.LocalStorage;
using Movieminds.Client.Providers;
using Movieminds.Client.Services;

namespace Movieminds.Client;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);
		builder.RootComponents.Add<App>("#app");
		builder.RootComponents.Add<HeadOutlet>("head::after");

		var services = builder.Services;

		services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5171/api/") });

		services.AddScoped<ILocalStorageService, LocalStorageService>();

		services.AddAuthorizationCore();
		services.AddScoped<JwtAuthenticationStateProvider>();
		services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<JwtAuthenticationStateProvider>());

		services.AddScoped<AuthenticationService>();

		services.AddScoped<UserProvider>();

		services.AddScoped<ProfileService>();
		services.AddScoped<ProfileProvider>();

		services.AddScoped<MovieService>();

		services.AddScoped<PostService>();

		services.AddScoped<MovieListService>();

		await builder.Build().RunAsync();
	}
}
