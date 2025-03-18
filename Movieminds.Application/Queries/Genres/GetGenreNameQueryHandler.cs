using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.Genres;

public class GetGenreNameQueryHandler : IQueryHandler<GetGenreNameQuery, IResponse<GetGenreNameResponse>>
{
	private readonly IRepositoryAsync<Genre> _genreRepository;

	public GetGenreNameQueryHandler(IUnitOfWorkAsync unitOfWork)
	{
		_genreRepository = unitOfWork.GetRepositoryAsync<Genre>();
	}

	public async Task<IResponse<GetGenreNameResponse>> HandleAsync(GetGenreNameQuery query)
	{
		try
		{
			var genre = await _genreRepository.GetByIdAsync(query.GenreId);

			return genre == null
				? Response.Fail<GetGenreNameResponse>("Genre not found")
				: Response.Ok(new GetGenreNameResponse(genre));
		}
		catch (Exception ex)
		{
			return Response.Fail<GetGenreNameResponse>(ex.Message);
		}
	}
}
