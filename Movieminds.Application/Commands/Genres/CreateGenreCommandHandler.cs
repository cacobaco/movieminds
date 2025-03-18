using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Genres;

public class CreateGenreCommandHandler : ICommandHandler<CreateGenreCommand, IResponse<CreateGenreResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWork;
    private readonly IRepositoryAsync<Genre> _genreRepository;

    public CreateGenreCommandHandler(IUnitOfWorkAsync unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _genreRepository = unitOfWork.GetRepositoryAsync<Genre>();
    }

    public async Task<IResponse<CreateGenreResponse>> HandleAsync(CreateGenreCommand request)
    {
        try
        {
            var genre = new Genre
            {
                Name = request.Name
            };

            await _genreRepository.InsertAsync(genre);
            await _unitOfWork.SaveChangesAsync();
            return Response.Ok(new CreateGenreResponse(genre));
        }
        catch (Exception ex)
        {
            return Response.Fail<CreateGenreResponse>(ex.Message);
        }
    }
}
