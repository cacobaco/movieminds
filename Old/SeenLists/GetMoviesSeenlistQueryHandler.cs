using Movieminds.Application.Contracts;
using Movieminds.Application.Requests;
using Movieminds.Domain.DTO;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.SeenLists;

public class GetMoviesSeenListQueryHandler : IQueryHandler<GetMoviesSeenListQuery, IResponse<GetMoviesSeenListResponse>>
{
	private readonly IRepositoryAsync<Profile> _profileRepository;
	private readonly IRepositoryAsync<SeenList> _seenListRepository;
	private readonly ITmdbService<dynamic> _tmdbService;
	private readonly IMovieAdaptor _movieAdaptor;

	public GetMoviesSeenListQueryHandler(IUnitOfWorkAsync unitOfWork, ITmdbService<dynamic> tmdbService, IMovieAdaptor movieAdaptor)
	{
		_profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
		_seenListRepository = unitOfWork.GetRepositoryAsync<SeenList>();
		_tmdbService = tmdbService;
		_movieAdaptor = movieAdaptor;
	}

	public async Task<IResponse<GetMoviesSeenListResponse>> HandleAsync(GetMoviesSeenListQuery query)
	{
		try
		{
			var profile = await _profileRepository.GetByIdAsync(query.UserId);
			if (profile == null)
			{
				return Response.Fail<GetMoviesSeenListResponse>("Profile not found");
			}

			var seenList = profile.SeenList;

			var responseMovies = new List<Movie>();
			foreach (var movie in seenList.Movies)
			{
				ApiResponse<dynamic> response = await _tmdbService.GetAsync($"movie/{movie.Id}?language=en-US");
				if (response.StatusCode != System.Net.HttpStatusCode.OK)
				{
					return Response.Fail<GetMoviesSeenListResponse>();
				}

				var responseMovie = _movieAdaptor.Adapt(response.Data);
				responseMovies.Add(responseMovie);
			}

			return Response.Ok(new GetMoviesSeenListResponse(responseMovies));
		}
		catch (Exception ex)
		{
			return Response.Fail<GetMoviesSeenListResponse>(ex.Message);
		}
	}
}
