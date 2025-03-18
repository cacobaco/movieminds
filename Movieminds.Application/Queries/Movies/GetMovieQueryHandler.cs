using Movieminds.Application.Commands.Movies;
using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.Movies;

public class GetMovieQueryHandler : ChainedRequestHandler<GetMovieQuery, IResponse<GetMovieResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWork;
    private readonly IRepositoryAsync<Movie> _movieRepository;

    public GetMovieQueryHandler(IUnitOfWorkAsync unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _movieRepository = unitOfWork.GetRepositoryAsync<Movie>();
    }

    public override async Task<IResponse<GetMovieResponse>> HandleAsync(GetMovieQuery request)
    {
        var movie = await _movieRepository.GetByIdAsync(request.Id);
        if (movie is not null)
        {
            var response = new GetMovieResponse(
                movie.Id,
                movie.Title,
                movie.Description,
                movie.PosterImageUrl,
                movie.Rating,
                movie.ReleaseDate
            );
            return Response.Ok(response);
        }

        try
        {
            IResponse<GetMovieResponse> nextResponse = await base.HandleAsync(request);
            if (!nextResponse.Success || nextResponse.Data is null)
            {
                return nextResponse;
            }

            var nextResponseMovie = nextResponse.Data;

            var saveCommand = new SaveExternalMovieCommand(nextResponseMovie);
            var saveHandler = new SaveExternalMovieCommandHandler(_unitOfWork);
            await saveHandler.HandleAsync(saveCommand);

            return nextResponse;
        }
        catch (NotImplementedException)
        {
            return Response.Fail<GetMovieResponse>("Movie not found");
        }
    }
}
