using Microsoft.AspNetCore.Mvc;
using Movieminds.Domain.Entities;
using Movieminds.Application.Requests;
using Movieminds.Application.Commands.WishLists;
using System.Security.Claims;

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

	[HttpGet("{id}")]
	public async Task<IActionResult> GetWishList(int id)
	{
		// var query = new GetMoviesWishListQuery(id);

		// var response = await _requestMediator.SendAsync(query);
		// if (!response.Success)
		// {
		// 	return NotFound();
		// }

		return Ok();
	}

	[HttpPost]
	public async Task<IActionResult> ToogleWishList(int MovieId)
	{
		var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		var command = new ToggleMovieWishListCommand(int.Parse(UserId), MovieId);

		var response = await _requestMediator.SendAsync(command);
		if (!response.Success)
		{
			return NotFound();
		}

		return Ok(response);
	}

	[HttpGet]
	public ActionResult<IEnumerable<WishList>> Get()
	{
		/*
		// Add movie to wishlist
		var ToggleMovieWishlistCommand = new ToggleMovieWishlistCommand(2, 36557);
		var response = _wishListService.AddWishListAsync(ToggleMovieWishlistCommand);

		// Add movie to wishlist
		var ToggleMovieWishlistCommand2 = new ToggleMovieWishlistCommand(2, 37724);
		var response2 = _wishListService.AddWishListAsync(ToggleMovieWishlistCommand2);

		// Add movie to wishlist
		var ToggleMovieWishlistCommand3 = new ToggleMovieWishlistCommand(2, 710);
		var response3 = _wishListService.AddWishListAsync(ToggleMovieWishlistCommand3);

		// Add movie to wishlist
		var ToggleMovieWishlistCommand4 = new ToggleMovieWishlistCommand(2, 37724);
		var response4 = _wishListService.AddWishListAsync(ToggleMovieWishlistCommand4);

		// Get Wishlist Movies
		var GetMoviesWishlistQuery = new GetMoviesWishlistQuery(2);
		var response5 = _wishListService.GetWishListAsync(GetMoviesWishlistQuery);
		var movies = response5.Result.Data.Movies;
		foreach (var movie in movies)
		{
			Console.WriteLine($"Movie ID: {movie.MovieId}, Title: {movie.Title}, Overview: {movie.Description}");
		}



		//Search movies by name
		var GetMoviesQuery = new SearchMovieNameQuery("Once upon a time", 1);
		var response6 = _movieService.GetMoviesByNameAsync(GetMoviesQuery);
		var movies = response6.Result.Data.Movies;
		foreach (var movie in movies)
		{
			Console.WriteLine($"Movie ID: {movie.MovieId}, Title: {movie.Title}, Genre: {movie.GenreIds[0]}, Descrição: {movie.Rating}\n");
		}

		//Get trending movies
		var GetTrendingMoviesQuery = new GetTrendingMoviesQuery();
		var response7 = _movieService.GetTrendingMovies(GetTrendingMoviesQuery);
		var movies = response7.Result.Data.Movies;
		foreach (var movie in movies)
		{
			Console.WriteLine($"Movie ID: {movie.MovieId}, Title: {movie.Title}, Genre: {movie.GenreIds[0]}, Id: {movie.MovieId}\n");
		}


		//Get movie by id
		var GetMovieByIdQuery = new GetMovieByIdQuery(36557);
		var response8 = _movieService.GetMovieByIdAsync(GetMovieByIdQuery);
		var movie = response8.Result.Data.Movie;
		Console.WriteLine($"Movie ID: {movie.MovieId}, Title: {movie.Title}, Genre: {movie.GenreIds[0]}, Id: {movie.MovieId}\n");

		//Get movie contributors
		var GetMovieContributorsQuery = new GetMovieContributorsQuery(36557);
		var response9 = _movieService.GetMovieContributorsAsync(GetMovieContributorsQuery);
		var contributors = response9.Result.Data.MovieContributors;
		foreach (var contributor in contributors)
		{
			Console.WriteLine($"Contributor Name: {contributor.Name}, Role: {contributor.role}");
		}
		*/
		return Ok();
	}
}
