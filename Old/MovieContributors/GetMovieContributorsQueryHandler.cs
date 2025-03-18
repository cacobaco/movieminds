using Movieminds.Application.Contracts;
using Movieminds.Application.Requests;
using Movieminds.Domain.DTO;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.MovieContributors;

public class GetMovieContributorsQueryHandler : IQueryHandler<GetMovieContributorsQuery, IResponse<GetMovieContributorsResponse>>
{
	private readonly IUnitOfWorkAsync _unitOfWork;
	private readonly ITmdbService<dynamic> _tmdbService;
	private readonly IMovieContributorAdaptor _movieContributorAdaptor;

	public GetMovieContributorsQueryHandler(IUnitOfWorkAsync unitOfWork, ITmdbService<dynamic> tmdbService, IMovieContributorAdaptor movieContributorAdaptor)
	{
		_unitOfWork = unitOfWork;
		_tmdbService = tmdbService;
		_movieContributorAdaptor = movieContributorAdaptor;
	}

	public async Task<IResponse<GetMovieContributorsResponse>> HandleAsync(GetMovieContributorsQuery query)
	{
		try
		{
			ApiResponse<dynamic> response = await _tmdbService.GetAsync($"movie/{query.MovieId}/credits?language=pt-PT");
			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				return Response.Fail<GetMovieContributorsResponse>();
			}

			var movieContributors = _movieContributorAdaptor.AdaptList(response.Data["cast"]);
			return Response.Ok(new GetMovieContributorsResponse(movieContributors));
		}
		catch (Exception ex)
		{
			return Response.Fail<GetMovieContributorsResponse>(ex.Message);
		}
	}
}
