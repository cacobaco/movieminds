using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Posts;

public class CreateMovieCommandHandler : ICommandHandler<CreateMovieCommand>
{
    private readonly IUnitOfWorkAsync _unitOfWork;
    private readonly IRepositoryAsync<Movie> _movieRepository;

    public CreateMovieCommandHandler(IUnitOfWorkAsync unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _movieRepository = unitOfWork.GetRepositoryAsync<Movie>();
    }

    public async Task<IResponse> HandleAsync(CreateMovieCommand request)
    {
        var existentMovie = await _movieRepository.GetByIdAsync(request.Id);
        if (existentMovie is not null)
        {
            return Response.Fail("Movie already exists.");
        }

        var movie = new Movie
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            PosterImageUrl = request.PosterImageUrl,
            Rating = request.Rating,
            Genres = request.Genres?.ToList() ?? [],
            Contributors = request.Contributors?.ToList() ?? [],
            ReleaseDate = request.ReleaseDate
        };

        await _movieRepository.InsertAsync(movie);
        await _unitOfWork.SaveChangesAsync();
        return Response.Ok();
    }
}
