using Microsoft.AspNetCore.Mvc;
using Movieminds.Application.Commands.Authentication;
using Movieminds.Application.Requests;
using Movieminds.Domain.Enums;
using Movieminds.Presentation.Requests.Authentication;

namespace Movieminds.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IRequestMediator _requestMediator;

    public AuthenticationController(IRequestMediator requestMediator)
    {
        _requestMediator = requestMediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var command = new LoginCommand(request.Identifier, request.Password);

        var response = await _requestMediator.SendAsync(command);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.Name,
            (Gender)request.Gender!,
            request.Email,
            (DateOnly)request.BirthDate!,
            request.Username,
            request.Password,
            request.ConfirmPassword
        );

        var response = await _requestMediator.SendAsync(command);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}
