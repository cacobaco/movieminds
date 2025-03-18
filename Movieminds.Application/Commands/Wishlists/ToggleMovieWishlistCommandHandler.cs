using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.WishLists;

public class ToggleMovieWishListCommandHandler : ICommandHandler<ToggleMovieWishListCommand>
{
	private readonly IUnitOfWorkAsync _unitOfWork;
	private readonly IRepositoryAsync<Profile> _profileRepository;
	private readonly IRepositoryAsync<Movie> _movieRepository;
	private readonly IRepositoryAsync<WishList> _wishListRepository;

	public ToggleMovieWishListCommandHandler(IUnitOfWorkAsync unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
		_movieRepository = unitOfWork.GetRepositoryAsync<Movie>();
		_wishListRepository = unitOfWork.GetRepositoryAsync<WishList>();
	}

	public async Task<IResponse> HandleAsync(ToggleMovieWishListCommand request)
	{
		try
		{
			var profile = await _profileRepository.GetByIdAsync(request.UserId);
			if (profile == null)
			{
				return Response.Fail("Profile not found");
			}

			var movie = await _movieRepository.GetByIdAsync(request.MovieId);
			if (movie == null)
			{
				return Response.Fail("Movie not found");
			}

			var wishList = profile.WishList;

			if (wishList == null)
			{
				return Response.Fail("WishList not found");
			}

			if (wishList.Movies.Contains(movie))
			{
				wishList.Movies.Remove(movie);
			}
			else
			{
				wishList.Movies.Add(movie);
			}

			_wishListRepository.Update(wishList);
			await _unitOfWork.SaveChangesAsync();
			return Response.Ok();
		}
		catch (Exception)
		{
			return Response.Fail("Failed to toggle movie");
		}
	}
}
