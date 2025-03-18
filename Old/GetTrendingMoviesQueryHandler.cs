using Movieminds.Application.Contracts;
using Movieminds.Application.Requests;
using Movieminds.Domain.DTO;
using Newtonsoft.Json.Linq;

namespace Movieminds.Application.Queries.Movies;

public class GetTrendingMoviesQueryHandler : IRequestHandler<GetTrendingMoviesQuery, IResponse<GetTrendingMoviesResponse>>
{
	private readonly ITmdbService<dynamic> _tmdbService;
	private readonly IMovieAdaptor _movieAdaptor;

	public GetTrendingMoviesQueryHandler(ITmdbService<dynamic> tmdbService, IMovieAdaptor movieAdaptor)
	{
		_tmdbService = tmdbService;
		_movieAdaptor = movieAdaptor;
	}

	public async Task<IResponse<GetTrendingMoviesResponse>> HandleAsync(GetTrendingMoviesQuery request)
	{
		try
		{
			ApiResponse<dynamic> response = await _tmdbService.GetAsync("trending/movie/day?language=pt-PT'");
			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				return Response.Fail<GetTrendingMoviesResponse>();
			}

			var movies = _movieAdaptor.AdaptList(response.Data["results"].ToObject<JArray>());
			return Response.Ok(new GetTrendingMoviesResponse(movies));
		}
		catch (Exception ex)
		{
			return Response.Fail<GetTrendingMoviesResponse>(ex.Message);
		}
	}
}
