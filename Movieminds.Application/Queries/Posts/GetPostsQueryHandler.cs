using Movieminds.Application.Queries.Movies;
using Movieminds.Application.Queries.Profiles;
using Movieminds.Application.Requests;
using Movieminds.Domain.Entities;
using Movieminds.Domain.Repositories;

namespace Movieminds.Application.Queries.Posts;

public class GetPostsQueryHandler : IQueryHandler<GetPostsQuery, ICollectionResponse<GetPostResponse>>
{
	private readonly IRepositoryAsync<Post> _postRepository;
	private readonly IRepositoryAsync<Profile> _profileRepository;

	public GetPostsQueryHandler(IUnitOfWorkAsync unitOfWork)
	{
		_postRepository = unitOfWork.GetRepositoryAsync<Post>();
		_profileRepository = unitOfWork.GetRepositoryAsync<Profile>();
	}

	public async Task<ICollectionResponse<GetPostResponse>> HandleAsync(GetPostsQuery request)
	{
		try
		{
			var posts = await _postRepository.GetAllAsync();

			if (request.ProfileId.HasValue)
			{
				foreach (var post in posts)
				{
					_postRepository.Ensure(post, p => p.Author);
				}
				posts = posts.Where(post => post.Author.Id == request.ProfileId.Value);
			}

			if (request.MovieId.HasValue)
			{
				foreach (var post in posts)
				{
					_postRepository.Ensure(post, p => p.Movie);
				}
				posts = posts.Where(post => post.Movie.Id == request.MovieId.Value);
			}

			var allPosts = posts.Select(post =>
			{
				if (!request.ProfileId.HasValue) _postRepository.Ensure(post, p => p.Author);
				_profileRepository.Ensure(post.Author, a => a.Owner);
				if (!request.MovieId.HasValue) _postRepository.Ensure(post, p => p.Movie);

				return new GetPostResponse(
					post.Id,
					new GetProfileResponse(
						post.Author.Id,
						post.Author.Owner.Name,
						post.Author.Name,
						post.Author.AvatarImageUrl,
						post.Author.BannerImageUrl,
						post.Author.IsPrivate
					),
					new GetMovieResponse(
						post.Movie.Id,
						post.Movie.Title,
						post.Movie.Description,
						post.Movie.PosterImageUrl,
						post.Movie.Rating,
						post.Movie.ReleaseDate
					),
					post.Content,
					post.LikedBy.Count,
					post.CreatedAt
				);
			}).OrderByDescending(post => post.Id);

			return CollectionResponse<GetPostResponse>.Ok(allPosts);
		}
		catch (Exception)
		{
			return CollectionResponse<GetPostResponse>.Fail("An error occurred while fetching posts.");
		}
	}
}
