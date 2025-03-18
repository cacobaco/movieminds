using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.Profiles;

public class GetProfileByUserIdQueryHandler : IQueryHandler<GetProfileByUserIdQuery, IResponse<GetProfileByUserIdResponse>>
{
	private readonly IRepositoryAsync<User> _userRepository;

	public GetProfileByUserIdQueryHandler(IUnitOfWorkAsync unitOfWork)
	{
		_userRepository = unitOfWork.GetRepositoryAsync<User>();
	}

	public async Task<IResponse<GetProfileByUserIdResponse>> HandleAsync(GetProfileByUserIdQuery request)
	{
		try
		{
			var user = await _userRepository.GetByIdAsync(request.UserId);
			if (user is null)
			{
				return Response.Fail<GetProfileByUserIdResponse>("User not found");
			}

			_userRepository.Ensure(user, u => u.Profile);

			return Response.Ok(new GetProfileByUserIdResponse(user.Profile));
		}
		catch (Exception)
		{
			return Response.Fail<GetProfileByUserIdResponse>("Failed to get profile");
		}
	}
}
