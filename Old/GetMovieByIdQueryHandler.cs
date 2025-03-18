using Movieminds.Application.Contracts;
using Movieminds.Application.Requests;
using Movieminds.Domain.DTO;

namespace Movieminds.Application.Queries.Movies;

public class GetMovieByIdQueryHandler : IQueryHandler<GetMovieByIdQuery, IResponse<GetMovieByIdResponse>>
{
	private readonly ITmdbService<dynamic> _tmdbService;
	private readonly IMovieAdaptor _movieAdaptor;

	public GetMovieByIdQueryHandler(ITmdbService<dynamic> tmdbService, IMovieAdaptor movieAdaptor)
	{
		_tmdbService = tmdbService;
		_movieAdaptor = movieAdaptor;
	}

	public async Task<IResponse<GetMovieByIdResponse>> HandleAsync(GetMovieByIdQuery query)
	{
		try
		{
			ApiResponse<dynamic> response = await _tmdbService.GetAsync($"movie/{query.Id}?language=pt-PT");
			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				return Response.Fail<GetMovieByIdResponse>();
			}

			var movie = _movieAdaptor.Adapt(response.Data);
			return Response.Ok(new GetMovieByIdResponse(movie));
		}
		catch (Exception ex)
		{
			return Response.Fail<GetMovieByIdResponse>(ex.Message);
		}
	}
}
