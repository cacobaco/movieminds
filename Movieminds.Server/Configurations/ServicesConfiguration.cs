using Microsoft.EntityFrameworkCore;
using Movieminds.Application.Commands.Authentication;
using Movieminds.Application.Commands.Posts;
using Movieminds.Application.Commands.SeenLists;
using Movieminds.Application.Commands.WishLists;
using Movieminds.Application.Queries.Movies;
using Movieminds.Application.Queries.Posts;
using Movieminds.Application.Queries.Profiles;
using Movieminds.Application.Queries.SeenLists;
using Movieminds.Application.Queries.WishLists;
using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;
using Movieminds.Infrastructure.Mappers;
using Movieminds.Infrastructure.Queries.Movies;
using Movieminds.Persistence;
using Movieminds.Persistence.Repositories;

namespace Movieminds.Server.Configurations;

public static class ServicesConfiguration
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var types = new Type[] {
            typeof(User),
            typeof(Profile),
            typeof(Genre),
            typeof(Movie),
            typeof(Post),
            typeof(WishList),
            typeof(SeenList),
            typeof(Message),
        };
        services.AddSingleton<IFactory>(sp => new Factory(types));

        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<DbContext, MoviemindsDbContext>(options => options.UseSqlite(connectionString), ServiceLifetime.Scoped);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUnitOfWorkAsync, UnitOfWorkAsync>();
    }

    public static void AddMappers(this IServiceCollection services)
    {
        services.AddScoped<TmdbMovieResponseMapper>();
    }

    public static void AddCommands(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<LoginCommand, IResponse<LoginResponse>>, LoginCommandHandler>();
        services.AddScoped<IRequestHandler<RegisterCommand, IResponse<RegisterResponse>>, RegisterCommandHandler>();

        services.AddScoped<IRequestHandler<CreatePostCommand>, CreatePostCommandHandler>();

        services.AddScoped<IRequestHandler<ToggleMovieWishListCommand>, ToggleMovieWishListCommandHandler>();
        services.AddScoped<IRequestHandler<ToggleMovieSeenListCommand>, ToggleMovieSeenListCommandHandler>();
    }

    public static void AddQueries(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<GetProfileQuery, IResponse<GetProfileResponse>>, GetProfileQueryHandler>();

        services.AddScoped<GetExternalMovieQueryHandler, GetTmdbMovieQueryHandler>();
        services.AddScoped<IRequestHandler<GetMovieQuery, IResponse<GetMovieResponse>>, GetMovieQueryHandler>((sp) =>
        {
            var handler = new GetMovieQueryHandler(sp.GetRequiredService<IUnitOfWorkAsync>());
            handler.SetNextHandler(sp.GetRequiredService<GetExternalMovieQueryHandler>());
            return handler;
        });

        services.AddScoped<GetExternalMoviesQueryHandler, GetTmdbMoviesQueryHandler>();
        services.AddScoped<IRequestHandler<GetMoviesQuery, IPaginatedResponse<GetMovieResponse>>, GetMoviesQueryHandler>((sp) =>
        {
            var handler = new GetMoviesQueryHandler(sp.GetRequiredService<IUnitOfWorkAsync>());
            handler.SetNextHandler(sp.GetRequiredService<GetExternalMoviesQueryHandler>());
            return handler;
        });

        services.AddScoped<GetExternalTrendingMoviesQueryHandler, GetTmdbTrendingMoviesQueryHandler>();
        services.AddScoped<IRequestHandler<GetTrendingMoviesQuery, IPaginatedResponse<GetMovieResponse>>, GetTrendingMoviesQueryHandler>((sp) =>
        {
            var handler = new GetTrendingMoviesQueryHandler(sp.GetRequiredService<IUnitOfWorkAsync>());
            handler.SetNextHandler(sp.GetRequiredService<GetExternalTrendingMoviesQueryHandler>());
            return handler;
        });

        services.AddScoped<IRequestHandler<GetPostsQuery, ICollectionResponse<GetPostResponse>>, GetPostsQueryHandler>();

        services.AddScoped<IRequestHandler<GetProfileWishListQuery, IResponse<GetWishListResponse>>, GetProfileWishListQueryHandler>();
        services.AddScoped<IRequestHandler<GetProfileSeenListQuery, IResponse<GetSeenListResponse>>, GetProfileSeenListQueryHandler>();
    }
}
