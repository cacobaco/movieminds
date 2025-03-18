using Movieminds.Application.Queries.Movies;
using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.SeenLists;

public class GetProfileSeenListQueryHandler : IQueryHandler<GetProfileSeenListQuery, IResponse<GetSeenListResponse>>
{
    private readonly IRepositoryAsync<SeenList> _seenListRepository;

    public GetProfileSeenListQueryHandler(IUnitOfWorkAsync unitOfWork)
    {
        _seenListRepository = unitOfWork.GetRepositoryAsync<SeenList>();
    }

    public async Task<IResponse<GetSeenListResponse>> HandleAsync(GetProfileSeenListQuery request)
    {
        var seenList = await _seenListRepository.GetFirstOrDefaultAsync(sl => sl.Owner.Id == request.ProfileId);
        if (seenList == null)
        {
            return Response.Fail<GetSeenListResponse>("Seen list not found");
        }

        _seenListRepository.Ensure(seenList, sl => (IEnumerable<Movie>)sl.Movies);

        var movies = seenList.Movies.Select(movie => new GetMovieResponse(
                movie.Id,
                movie.Title,
                movie.Description,
                movie.PosterImageUrl,
                movie.Rating,
                movie.ReleaseDate
            )
        );

        return Response.Ok(new GetSeenListResponse(
            seenList.Id,
            movies
        ));
    }
}
