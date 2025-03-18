using Movieminds.Application.Queries.Movies;
using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Movies;

public class SaveExternalMoviesCommandHandler : ICommandHandler<SaveExternalMoviesCommand>
{
    private readonly IUnitOfWorkAsync _unitOfWork;
    private readonly IRepositoryAsync<Movie> _movieRepository;

    public SaveExternalMoviesCommandHandler(IUnitOfWorkAsync unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _movieRepository = unitOfWork.GetRepositoryAsync<Movie>();
    }
    public async Task<IResponse> HandleAsync(SaveExternalMoviesCommand request)
    {
        var externalResponses = request.Response;

        var saveableMovies = externalResponses.Where((externalResponse) =>
        {
            var existentMovie = _movieRepository.GetById(externalResponse.Id);
            return existentMovie is null;
        });

        var movies = saveableMovies.Select((externalResponse) => new Movie
        {
            Id = externalResponse.Id,
            Title = externalResponse.Title,
            Description = externalResponse.Description,
            PosterImageUrl = externalResponse.PosterImageUrl,
            Rating = externalResponse.Rating,
            Genres = [], // TODO
            Contributors = [], // TODO
            ReleaseDate = externalResponse.ReleaseDate
        });

        await _movieRepository.InsertBulkAsync(movies);
        await _unitOfWork.SaveChangesAsync();
        return Response.Ok(externalResponses);
    }
}
