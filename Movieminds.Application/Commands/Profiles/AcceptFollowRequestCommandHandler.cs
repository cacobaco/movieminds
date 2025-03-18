using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Profiles;

public class AcceptFollowRequestCommandHandler : ICommandHandler<AcceptFollowRequestCommand>
{
	private readonly IUnitOfWorkAsync _unitOfWork;
	private readonly IRepositoryAsync<Profile> _profileRepository;

	public AcceptFollowRequestCommandHandler(IUnitOfWorkAsync unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
	}

	public async Task<IResponse> HandleAsync(AcceptFollowRequestCommand request)
	{
		try
		{
			var senderProfile = await _profileRepository.GetByIdAsync(request.SenderProfileId);
			if (senderProfile == null)
			{
				return Response.Fail("Sender profile not found");
			}

			var receiverProfile = await _profileRepository.GetByIdAsync(request.ReceiverProfileId);
			if (receiverProfile == null)
			{
				return Response.Fail("Receiver profile not found");
			}

			if (!receiverProfile.ReceivedFollowRequests.Contains(senderProfile))
			{
				return Response.Fail("Follow request not found");
			}

			try
			{
				_unitOfWork.Begin();

				receiverProfile.ReceivedFollowRequests.Remove(senderProfile);
				receiverProfile.Followers.Add(senderProfile);
				senderProfile.SentFollowRequests.Remove(receiverProfile);
				senderProfile.Followings.Add(receiverProfile);

				await _unitOfWork.SaveChangesAsync();
				_unitOfWork.Commit();
				return Response.Ok();
			}
			catch (Exception)
			{
				_unitOfWork.Rollback();
				return Response.Fail("Failed to accept follow request");
			}
		}
		catch (Exception)
		{
			return Response.Fail("Failed to accept follow request");
		}
	}
}
