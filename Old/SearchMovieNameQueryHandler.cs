using Movieminds.Application.Contracts;
using Movieminds.Application.Requests;
using Movieminds.Domain.DTO;
using Newtonsoft.Json.Linq;

namespace Movieminds.Application.Queries.Movies;

public class SearchMovieNameQueryHandler : IRequestHandler<SearchMovieNameQuery, IResponse<SearchMovieNameResponse>>
{
	private readonly ITmdbService<dynamic> _tmdbService;
	private readonly IMovieAdaptor _movieAdaptor;

	public SearchMovieNameQueryHandler(ITmdbService<dynamic> tmdbService, IMovieAdaptor movieAdaptor)
	{
		_tmdbService = tmdbService;
		_movieAdaptor = movieAdaptor;
	}

	public async Task<IResponse<SearchMovieNameResponse>> HandleAsync(SearchMovieNameQuery request)
	{
		try
		{
			var nameQuery = request.Name.Replace(" ", "%20");
			ApiResponse<dynamic> response = await _tmdbService.GetAsync($"search/movie?query={nameQuery}&include_adult=false&language=pt-PT&page={request.PageNumber}'");
			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				return Response.Fail<SearchMovieNameResponse>();
			}

			var movies = _movieAdaptor.AdaptList(response.Data["results"].ToObject<JArray>());
			return Response.Ok(new SearchMovieNameResponse(movies));
		}
		catch (Exception ex)
		{
			return Response.Fail<SearchMovieNameResponse>(ex.Message);
		}
	}
}