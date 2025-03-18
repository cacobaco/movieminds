using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Genres;

public class RemoveAllGenresCommandHandler : ICommandHandler<RemoveAllGenresCommand>
{
	private readonly IUnitOfWorkAsync _unitOfWork;
	private readonly IRepositoryAsync<Genre> _genreRepository;

	public RemoveAllGenresCommandHandler(IUnitOfWorkAsync unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_genreRepository = unitOfWork.GetRepositoryAsync<Genre>();
	}

	public async Task<IResponse> HandleAsync(RemoveAllGenresCommand request)
	{
		try
		{
			//Get all genres from the database
			var genres = await _genreRepository.GetAllAsync();
			if (genres == null)
			{
				return Response.Fail("Genres not found");
			}

			//Remove all genres from the database
			foreach (var genre in genres)
			{
				_genreRepository.Delete(genre);
			}
			await _unitOfWork.SaveChangesAsync();

			return Response.Ok();
		}
		catch (Exception)
		{
			return Response.Fail("Failed to remove the genres");
		}
	}
}
