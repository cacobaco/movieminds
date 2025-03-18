using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Commands.Posts;

public class CreatePostCommandHandler : ICommandHandler<CreatePostCommand>
{
	private readonly IUnitOfWorkAsync _unitOfWork;
	private readonly IRepositoryAsync<Movie> _movieRepository;
	private readonly IRepositoryAsync<Post> _postRepository;

	public CreatePostCommandHandler(IUnitOfWorkAsync unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_movieRepository = unitOfWork.GetRepositoryAsync<Movie>();
		_postRepository = unitOfWork.GetRepositoryAsync<Post>();
	}

	public async Task<IResponse> HandleAsync(CreatePostCommand request)
	{
		Console.WriteLine("3");
		try
		{
			var author = await _unitOfWork.GetRepositoryAsync<Profile>().GetByIdAsync(request.AuthorId);
			if (author == null)
			{
				return Response.Fail("Author not found");
			}

			var movie = await _movieRepository.GetByIdAsync(request.MovieId);
			if (movie == null)
			{
				return Response.Fail("Movie not found");
			}

			var post = new Post
			{
				Author = author,
				Movie = movie,
				Content = request.Content,
			};

			await _postRepository.InsertAsync(post);
			await _unitOfWork.SaveChangesAsync();
			Console.WriteLine("4");
			return Response.Ok();
		}
		catch (Exception)
		{
			return Response.Fail("Failed to create a new post");
		}
	}
}
