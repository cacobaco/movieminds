using Microsoft.AspNetCore.Authentication.JwtBearer;
using Movieminds.Server.Configurations;
using Movieminds.Application.Contracts;
using Movieminds.Infrastructure.Authentication;
using Movieminds.Application.Requests;
using Movieminds.Infrastructure.Extensions;

namespace Movieminds.Server;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		var services = builder.Services;
		var configuration = builder.Configuration;

		services.AddCors(options =>
		{
			options.AddDefaultPolicy(builder => builder
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader()
			);
		});

		services.ConfigureOptions<JwtConfiguration>();
		services.ConfigureOptions<JwtBearerConfiguration>();
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer();
		services.AddSingleton<IJwtProvider, JwtProvider>();

		services.ConfigureOptions<TmdbConfiguration>();

		services.AddScoped<TmdbHttpClient>();

		services.AddRepositories(configuration);
		services.AddMappers();
		services.AddCommands();
		services.AddQueries();

		services.AddScoped<IRequestMediator, RequestMediator>(sp => new RequestMediator(sp));

		services.AddControllers();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		// app.UseHttpsRedirection();

		app.UseCors();
		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllers();
		app.Run();
	}
}
