using Microsoft.AspNetCore.Mvc;
using Movieminds.Domain.Entities;
using Movieminds.Application.Requests;
using Movieminds.Application.Commands.SeenLists;
using System.Security.Claims;

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

	[HttpGet("{id}")]
	public async Task<IActionResult> GetSeenList(int id)
	{
		// var query = new GetMoviesSeenListQuery(id);

		// var response = await _requestMediator.SendAsync(query);
		// if (!response.Success)
		// {
		// 	return NotFound();
		// }

		return Ok();
	}

	[HttpPost]
	public async Task<IActionResult> ToggleSeenList(int MovieId)
	{
		var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		var command = new ToggleMovieSeenListCommand(int.Parse(UserId), MovieId);

		var response = await _requestMediator.SendAsync(command);
		if (!response.Success)
		{
			return NotFound();
		}

		return Ok(response);
	}

	[HttpGet]
	public ActionResult<IEnumerable<SeenList>> Get()
	{
		/*
		// Add movie to seenlist
		var ToggleMovieSeenlistCommand = new ToggleMovieSeenlistCommand(3, 36557);
		var response = _seenListService.AddSeenListAsync(ToggleMovieSeenlistCommand);

		// Add movie to seenlist
		var ToggleMovieSeenlistCommand2 = new ToggleMovieSeenlistCommand(3, 37724);
		var response2 = _seenListService.AddSeenListAsync(ToggleMovieSeenlistCommand2);

		// Add movie to seenlist
		var ToggleMovieSeenlistCommand3 = new ToggleMovieSeenlistCommand(3, 710);
		var response3 = _seenListService.AddSeenListAsync(ToggleMovieSeenlistCommand3);

		// Add movie to seenlist
		var ToggleMovieSeenlistCommand4 = new ToggleMovieSeenlistCommand(3, 37724);
		var response4 = _seenListService.AddSeenListAsync(ToggleMovieSeenlistCommand4);
		

		// Get Seenlist Movies
		var GetMoviesSeenlistQuery = new GetMoviesSeenlistQuery(2);
		var response5 = _seenListService.GetSeenListAsync(GetMoviesSeenlistQuery);
		var movies = response5.Result.Data.Movies;
		foreach (var movie in movies)
		{
			Console.WriteLine($"Movie ID: {movie.MovieId}, Title: {movie.Title}, Overview: {movie.Description}");
		}
		*/

		return Ok();
	}
}
