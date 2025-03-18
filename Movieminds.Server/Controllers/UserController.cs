using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movieminds.Domain.Entities;
using Movieminds.Application.Requests;

namespace Movieminds.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly IRequestMediator _requestMediator;

	public UserController(IRequestMediator requestMediator)
	{
		_requestMediator = requestMediator;
	}

	[HttpGet]
	public ActionResult<IEnumerable<User>> Get()
	{
		return Ok();
	}
}
