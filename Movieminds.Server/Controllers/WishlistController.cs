using Microsoft.AspNetCore.Mvc;
using Movieminds.Application.Requests;
using System.Security.Claims;
using Movieminds.Presentation.Requests.MovieLists;
using Movieminds.Application.Commands.WishLists;
using Movieminds.Application.Queries.WishLists;
using Microsoft.AspNetCore.Authorization;

namespace Movieminds.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WishListController : ControllerBase
{
	private readonly IRequestMediator _requestMediator;

	public WishListController(IRequestMediator requestMediator)
	{
		_requestMediator = requestMediator;
	}

	[HttpGet("profile/{id}")]
	public async Task<ActionResult> GetProfileWishList(int id)
	{
		var query = new GetProfileWishListQuery(id);

		var response = await _requestMediator.SendAsync(query);
		if (response is null)
		{
			return NotFound(response);
		}

		return Ok(response);
	}

	[HttpPost]
	[Authorize]
	public async Task<ActionResult> ToggleMovieWishList(ToggleMovieMovieListRequest request)
	{
		var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		var command = new ToggleMovieWishListCommand(int.Parse(userId), request.MovieId);

		var response = await _requestMediator.SendAsync(command);
		if (response is null)
		{
			return BadRequest(response);
		}

		return Ok(response);
	}
}
