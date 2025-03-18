using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.Users;

public class GetUsersByNameQueryHandler : IQueryHandler<GetUsersByNameQuery, IResponse<GetUsersByNameResponse>>
{
	private readonly IRepositoryAsync<User> _userRepository;

	public GetUsersByNameQueryHandler(IUnitOfWorkAsync unitOfWork)
	{
		_userRepository = unitOfWork.GetRepositoryAsync<User>();
	}

	public async Task<IResponse<GetUsersByNameResponse>> HandleAsync(GetUsersByNameQuery request)
	{
		try
		{
			if (string.IsNullOrEmpty(request.Query))
			{
				return Response.Fail<GetUsersByNameResponse>("Invalid request");
			}

			var users = await _userRepository.GetAllAsync();
			var usersByName = users.Where(u => u.Name.Contains(request.Query)).ToList();

			return Response.Ok(new GetUsersByNameResponse(usersByName));
		}
		catch (Exception)
		{
			return Response.Fail<GetUsersByNameResponse>("Failed to get users by name");
		}
	}
}
