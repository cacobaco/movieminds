using Movieminds.Application.Queries.Movies;
using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Movies;

public class SaveExternalMovieCommandHandler : ICommandHandler<SaveExternalMovieCommand>
{
    private readonly IUnitOfWorkAsync _unitOfWork;
    private readonly IRepositoryAsync<Movie> _movieRepository;

    public SaveExternalMovieCommandHandler(IUnitOfWorkAsync unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _movieRepository = unitOfWork.GetRepositoryAsync<Movie>();
    }
    public async Task<IResponse> HandleAsync(SaveExternalMovieCommand request)
    {
        var externalResponse = request.Response;

        var existentMovie = await _movieRepository.GetByIdAsync(externalResponse.Id);
        if (existentMovie is not null)
        {
            return Response.Fail("Movie already exists.");
        }

        var movie = new Movie
        {
            Id = externalResponse.Id,
            Title = externalResponse.Title,
            Description = externalResponse.Description,
            PosterImageUrl = externalResponse.PosterImageUrl,
            Rating = externalResponse.Rating,
            Genres = [], // TODO
            Contributors = [], // TODO
            ReleaseDate = externalResponse.ReleaseDate
        };

        await _movieRepository.InsertAsync(movie);
        await _unitOfWork.SaveChangesAsync();
        return Response.Ok(externalResponse);
    }
}
