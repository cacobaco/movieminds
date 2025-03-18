using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.Posts;

public class GetUserPostsQueryHandler : IQueryHandler<GetUserPostsQuery, IResponse<GetUserPostsResponse>>
{
	private readonly IRepositoryAsync<Profile> _profileRepository;
	private readonly IRepositoryAsync<Post> _postRepository;

	public GetUserPostsQueryHandler(IUnitOfWorkAsync unitOfWork)
	{
		_profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
		_postRepository = unitOfWork.GetRepositoryAsync<Post>();
	}

	public async Task<IResponse<GetUserPostsResponse>> HandleAsync(GetUserPostsQuery request)
	{
		try
		{
			var profile = await _profileRepository.GetByIdAsync(request.UserId);
			if (profile == null)
			{
				return Response.Fail<GetUserPostsResponse>("User not found");
			}

			var posts = await _postRepository.GetAllAsync();
			var userPosts = posts.Where(p => p.Author == profile).ToList();
			return Response.Ok(new GetUserPostsResponse(userPosts));
		}
		catch (Exception)
		{
			return Response.Fail<GetUserPostsResponse>("Failed to get user posts");
		}
	}
}
