using Movieminds.Application.Queries.Movies;
using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.WishLists;

public class GetProfileWishListQueryHandler : IQueryHandler<GetProfileWishListQuery, IResponse<GetWishListResponse>>
{
    private readonly IRepositoryAsync<WishList> _wishListRepository;

    public GetProfileWishListQueryHandler(IUnitOfWorkAsync unitOfWork)
    {
        _wishListRepository = unitOfWork.GetRepositoryAsync<WishList>();
    }

    public async Task<IResponse<GetWishListResponse>> HandleAsync(GetProfileWishListQuery request)
    {
        var wishList = await _wishListRepository.GetFirstOrDefaultAsync(sl => sl.Owner.Id == request.ProfileId);
        if (wishList == null)
        {
            return Response.Fail<GetWishListResponse>("Seen list not found");
        }

        _wishListRepository.Ensure(wishList, sl => (IEnumerable<Movie>)sl.Movies);

        var movies = wishList.Movies.Select(movie => new GetMovieResponse(
                movie.Id,
                movie.Title,
                movie.Description,
                movie.PosterImageUrl,
                movie.Rating,
                movie.ReleaseDate
            )
        );

        return Response.Ok(new GetWishListResponse(
            wishList.Id,
            movies
        ));
    }
}
