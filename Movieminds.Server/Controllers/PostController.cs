using Microsoft.AspNetCore.Mvc;

using Movieminds.Domain.Entities;
using Movieminds.Application.Commands.Posts;
using Movieminds.Application.Queries.Posts;
using Movieminds.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Movieminds.Presentation.Requests.Posts;

namespace Movieminds.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
	private readonly IRequestMediator _requestMediator;

	public PostController(IRequestMediator requestMediator)
	{
		_requestMediator = requestMediator;
	}

	[HttpGet]
	public async Task<IActionResult> GetPosts(int? profileId, int? movieId)
	{
		var query = new GetPostsQuery(
			profileId,
			movieId
		);

		var response = await _requestMediator.SendAsync(query);
		if (!response.Success)
		{
			return NotFound(response);
		}

		return Ok(response);
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> PostPost([FromBody] PostRequest request)
	{
		var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		var command = new CreatePostCommand(int.Parse(userId), (int)request.MovieId!, request.Content);

		var response = await _requestMediator.SendAsync(command);
		if (!response.Success)
		{
			return NotFound(response);
		}

		return Ok(response);
	}
}
