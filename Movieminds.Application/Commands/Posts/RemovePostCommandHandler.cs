using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Posts;

public class RemovePostCommandHandler : ICommandHandler<RemovePostCommand>
{
	private readonly IUnitOfWorkAsync _unitOfWork;
	private readonly IRepositoryAsync<Post> _postRepository;

	public RemovePostCommandHandler(IUnitOfWorkAsync unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_postRepository = unitOfWork.GetRepositoryAsync<Post>();
	}

	public async Task<IResponse> HandleAsync(RemovePostCommand request)
	{
		try
		{
			var post = await _postRepository.GetByIdAsync(request.PostId);

			if (post == null)
			{
				return Response.Fail("Post not found");
			}

			_postRepository.SoftDelete(post);
			await _unitOfWork.SaveChangesAsync();
			return Response.Ok();
		}
		catch (Exception)
		{
			return Response.Fail("Failed to remove the post");
		}
	}
}
