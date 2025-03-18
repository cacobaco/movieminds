using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Profiles;

public sealed record SendFollowRequestCommandHandler : ICommandHandler<SendFollowRequestCommand>
{
	private readonly IUnitOfWorkAsync _unitOfWork;
	private readonly IRepositoryAsync<Profile> _profileRepository;

	public SendFollowRequestCommandHandler(IUnitOfWorkAsync unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
	}

	public async Task<IResponse> HandleAsync(SendFollowRequestCommand request)
	{
		try
		{
			var senderProfile = await _profileRepository.GetByIdAsync(request.SenderProfileId);
			if (senderProfile == null)
			{
				return Response.Fail("Profile not found");
			}

			var receiverProfile = await _profileRepository.GetByIdAsync(request.ReceiverProfileId);
			if (receiverProfile == null)
			{
				return Response.Fail("Profile not found");
			}

			if (senderProfile.Followings.Contains(receiverProfile))
			{
				return Response.Fail("Already following");
			}

			if (senderProfile.SentFollowRequests.Contains(receiverProfile))
			{
				return Response.Fail("Request already sent");
			}

			try
			{
				_unitOfWork.Begin();

				senderProfile.SentFollowRequests.Add(receiverProfile);
				receiverProfile.ReceivedFollowRequests.Add(senderProfile);

				await _unitOfWork.SaveChangesAsync();

				_unitOfWork.Commit();
				return Response.Ok();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				return Response.Fail("Failed to send follow request");
			}
		}
		catch (Exception)
		{
			return Response.Fail("Failed to send follow request");
		}
	}
}
