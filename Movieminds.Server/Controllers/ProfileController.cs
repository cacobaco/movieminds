using Microsoft.AspNetCore.Mvc;
using Movieminds.Application.Queries.Profiles;
using Movieminds.Application.Requests;

namespace Movieminds.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IRequestMediator _requestMediator;

    public ProfileController(IRequestMediator requestMediator)
    {
        _requestMediator = requestMediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProfiles()
    {
        var query = new GetProfilesQuery();

        var response = await _requestMediator.SendAsync(query);
        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfile(int id)
    {
        var query = new GetProfileQuery(id);

        var response = await _requestMediator.SendAsync(query);
        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }
}
