using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Posts;

public class ToggleLikeCommandHandler : ICommandHandler<ToggleLikeCommand>
{
	private readonly IUnitOfWorkAsync _unitOfWork;
	private readonly IRepositoryAsync<Profile> _profileRepository;
	private readonly IRepositoryAsync<Post> _postRepository;

	public ToggleLikeCommandHandler(IUnitOfWorkAsync unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
		_postRepository = unitOfWork.GetRepositoryAsync<Post>();
	}

	public async Task<IResponse> HandleAsync(ToggleLikeCommand request)
	{
		try
		{
			var profile = await _profileRepository.GetByIdAsync(request.UserId);
			if (profile == null)
			{
				return Response.Fail("Profile not found");
			}

			var post = await _postRepository.GetByIdAsync(request.PostId);
			if (post == null)
			{
				return Response.Fail("Post not found");
			}

			if (post.LikedBy.Contains(profile))
			{
				post.LikedBy.Remove(profile);
			}
			else
			{
				post.LikedBy.Add(profile);
			}

			_postRepository.Update(post);
			await _unitOfWork.SaveChangesAsync();
			return Response.Ok();
		}
		catch (Exception)
		{
			return Response.Fail("Failed to toggle like");
		}
	}
}
