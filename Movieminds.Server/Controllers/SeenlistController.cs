using Microsoft.AspNetCore.Mvc;
using Movieminds.Application.Requests;
using Movieminds.Application.Commands.SeenLists;
using System.Security.Claims;
using Movieminds.Application.Queries.SeenLists;
using Movieminds.Presentation.Requests.MovieLists;
using Microsoft.AspNetCore.Authorization;

namespace Movieminds.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeenListController : ControllerBase
{
	private readonly IRequestMediator _requestMediator;

	public SeenListController(IRequestMediator requestMediator)
	{
		_requestMediator = requestMediator;
	}

	[HttpGet("profile/{id}")]
	public async Task<ActionResult> GetProfileSeenList(int id)
	{
		var query = new GetProfileSeenListQuery(id);

		var response = await _requestMediator.SendAsync(query);
		if (response is null)
		{
			return NotFound(response);
		}

		return Ok(response);
	}

	[HttpPost]
	[Authorize]
	public async Task<ActionResult> ToggleMovieSeenList(ToggleMovieMovieListRequest request)
	{
		var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		var command = new ToggleMovieSeenListCommand(int.Parse(userId), request.MovieId);

		var response = await _requestMediator.SendAsync(command);
		if (response is null)
		{
			return BadRequest(response);
		}

		return Ok(response);
	}
}
