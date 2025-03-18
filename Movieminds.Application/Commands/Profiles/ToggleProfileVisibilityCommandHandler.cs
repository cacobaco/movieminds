using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Profiles;

public class ToggleProfileVisibilityCommandHandler : ICommandHandler<ToggleProfileVisibilityCommand>
{
	private readonly IUnitOfWorkAsync _unitOfWork;
	private readonly IRepositoryAsync<Profile> _profileRepository;

	public ToggleProfileVisibilityCommandHandler(IUnitOfWorkAsync unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
	}

	public async Task<IResponse> HandleAsync(ToggleProfileVisibilityCommand request)
	{
		try
		{
			var profile = await _profileRepository.GetByIdAsync(request.ProfileId);
			if (profile == null)
			{
				return Response.Fail("Profile not found");
			}

			profile.IsPrivate = !profile.IsPrivate;

			_profileRepository.Update(profile);
			await _unitOfWork.SaveChangesAsync();
			return Response.Ok();
		}
		catch (Exception)
		{
			return Response.Fail("Failed to toggle profile visibility");
		}
	}
}
