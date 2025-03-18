using Microsoft.AspNetCore.Mvc;
using Movieminds.Application.Queries.Movies;
using Movieminds.Application.Requests;
using Movieminds.Presentation.Requests.Movies;

namespace Movieminds.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly IRequestMediator _requestMediator;

    public MovieController(IRequestMediator requestMediator)
    {
        _requestMediator = requestMediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetMovies([FromQuery] MoviesRequest request)
    {
        var query = new GetMoviesQuery(
            request.Search,
            request.SortBy,
            request.PageNumber,
            request.PageSize
        );

        var response = await _requestMediator.SendAsync(query);
        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpGet("trending")]
    public async Task<IActionResult> GetTrendingMovies([FromQuery] MoviesRequest request)
    {
        var query = new GetMoviesQuery(
            request.Search,
            request.SortBy,
            request.PageNumber,
            request.PageSize
        );

        var response = await _requestMediator.SendAsync(query);
        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovie(int id)
    {
        var query = new GetMovieQuery(id);

        var response = await _requestMediator.SendAsync(query);
        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }
}
