using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.Profiles;

public sealed record GetProfileQueryHandler : IQueryHandler<GetProfileQuery, IResponse<GetProfileResponse>>
{
	private readonly IRepositoryAsync<Profile> _profileRepository;

	public GetProfileQueryHandler(IUnitOfWorkAsync unitOfWork)
	{
		_profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
	}

	public async Task<IResponse<GetProfileResponse>> HandleAsync(GetProfileQuery request)
	{
		try
		{
			var profile = await _profileRepository.GetByIdAsync(request.ProfileId);
			if (profile == null)
			{
				return Response.Fail<GetProfileResponse>("Profile not found");
			}

			_profileRepository.Ensure(profile, p => p.Owner);

			var response = new GetProfileResponse(
				profile.Id,
				profile.Owner.Name,
				profile.Name,
				profile.AvatarImageUrl,
				profile.BannerImageUrl,
				profile.IsPrivate
			);
			return Response.Ok(response);
		}
		catch (Exception)
		{
			return Response.Fail<GetProfileResponse>("Failed to get profile");
		}
	}
}
