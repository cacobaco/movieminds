using Movieminds.Application.Contracts;
using Movieminds.Application.Requests;
using Movieminds.Domain.DTO;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.WishLists;

public class GetMoviesWishListQueryHandler : IQueryHandler<GetMoviesWishListQuery, IResponse<GetMoviesWishListResponse>>
{
	private readonly IRepositoryAsync<Profile> _profileRepository;
	private readonly IRepositoryAsync<WishList> _wishListRepository;
	private readonly ITmdbService<dynamic> _tmdbService;
	private readonly IMovieAdaptor _movieAdaptor;

	public GetMoviesWishListQueryHandler(IUnitOfWorkAsync unitOfWork, ITmdbService<dynamic> tmdbService, IMovieAdaptor movieAdaptor)
	{
		_profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
		_wishListRepository = unitOfWork.GetRepositoryAsync<WishList>();
		_tmdbService = tmdbService;
		_movieAdaptor = movieAdaptor;
	}

	public async Task<IResponse<GetMoviesWishListResponse>> HandleAsync(GetMoviesWishListQuery query)
	{
		try
		{
			var profile = await _profileRepository.GetByIdAsync(query.UserId);
			if (profile == null)
			{
				return Response.Fail<GetMoviesWishListResponse>("Profile not found");
			}

			var wishlist = profile.WishList;

			var responseMovies = new List<Movie>();
			foreach (var movie in wishlist.Movies)
			{
				ApiResponse<dynamic> response = await _tmdbService.GetAsync($"movie/{movie.Id}?language=en-US");
				if (response.StatusCode != System.Net.HttpStatusCode.OK)
				{
					return Response.Fail<GetMoviesWishListResponse>();
				}

				var responseMovie = _movieAdaptor.Adapt(response.Data);
				responseMovies.Add(movie);
			}

			return Response.Ok(new GetMoviesWishListResponse(responseMovies));
		}
		catch (Exception ex)
		{
			return Response.Fail<GetMoviesWishListResponse>(ex.Message);
		}
	}
}
