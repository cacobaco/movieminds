using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Profiles;

public class ChangeProfilePictureCommandHandler : ICommandHandler<ChangeProfilePictureCommand>
{
	private readonly IUnitOfWorkAsync _unitOfWork;
	private readonly IRepositoryAsync<Profile> _profileRepository;

	public ChangeProfilePictureCommandHandler(IUnitOfWorkAsync unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
	}

	public async Task<IResponse> HandleAsync(ChangeProfilePictureCommand request)
	{
		try
		{
			var profile = await _profileRepository.GetByIdAsync(request.ProfileId);
			if (profile == null)
			{
				return Response.Fail("Profile not found");
			}

			profile.AvatarImageUrl = request.ImageUrl;

			_profileRepository.Update(profile);
			await _unitOfWork.SaveChangesAsync();
			return Response.Ok();
		}
		catch (Exception)
		{
			return Response.Fail("Failed to change profile picture");
		}
	}
}
