using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.Profiles;

public sealed record GetProfilesQueryHandler : IQueryHandler<GetProfilesQuery, ICollectionResponse<GetProfileResponse>>
{
	private readonly IRepositoryAsync<Profile> _profileRepository;

	public GetProfilesQueryHandler(IUnitOfWorkAsync unitOfWork)
	{
		_profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
	}

	public async Task<ICollectionResponse<GetProfileResponse>> HandleAsync(GetProfilesQuery request)
	{
		try
		{
			var profiles = await _profileRepository.GetAllAsync();
			if (profiles is null)
			{
				return CollectionResponse<GetProfileResponse>.Ok([]);
			}

			var response = profiles.Select(profile =>
			{
				_profileRepository.Ensure(profile, p => p.Owner);

				return new GetProfileResponse(
					profile.Id,
					profile.Owner.Name,
					profile.Name,
					profile.AvatarImageUrl,
					profile.BannerImageUrl,
					profile.IsPrivate
				);
			});
			return CollectionResponse<GetProfileResponse>.Ok(response);
		}
		catch (Exception)
		{
			return CollectionResponse<GetProfileResponse>.Fail("An error occurred while fetching profiles.");
		}
	}
}
