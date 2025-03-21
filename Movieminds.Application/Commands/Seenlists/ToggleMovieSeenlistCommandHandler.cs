﻿using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.SeenLists;

public class ToggleMovieSeenListCommandHandler : ICommandHandler<ToggleMovieSeenListCommand>
{
	private readonly IUnitOfWorkAsync _unitOfWork;
	private readonly IRepositoryAsync<Profile> _profileRepository;
	private readonly IRepositoryAsync<Movie> _movieRepository;
	private readonly IRepositoryAsync<SeenList> _seenListRepository;

	public ToggleMovieSeenListCommandHandler(IUnitOfWorkAsync unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
		_movieRepository = unitOfWork.GetRepositoryAsync<Movie>();
		_seenListRepository = unitOfWork.GetRepositoryAsync<SeenList>();
	}

	public async Task<IResponse> HandleAsync(ToggleMovieSeenListCommand request)
	{
		try
		{
			var profile = await _profileRepository.GetByIdAsync(request.ProfileId);
			if (profile == null)
			{
				return Response.Fail("Profile not found");
			}

			var movie = await _movieRepository.GetByIdAsync(request.MovieId);
			if (movie == null)
			{
				return Response.Fail("Movie not found");
			}

			_profileRepository.Ensure(profile, p => p.SeenList);

			var seenList = profile.SeenList;
			if (seenList == null)
			{
				return Response.Fail("SeenList not found");
			}

			_seenListRepository.Ensure(profile.SeenList, s => (IEnumerable<Movie>)s.Movies);

			var removed = false;
			if (seenList.Movies.Contains(movie))
			{
				seenList.Movies.Remove(movie);
				removed = true;
			}
			else
			{
				seenList.Movies.Add(movie);
			}

			_seenListRepository.Update(seenList);
			await _unitOfWork.SaveChangesAsync();
			return Response.Ok("Movie " + (removed ? "removed" : "added") + " to seen list");
		}
		catch (Exception)
		{
			return Response.Fail("Failed to toggle movie");
		}
	}
}
