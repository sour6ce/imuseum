
using Microsoft.AspNetCore.Mvc;
using IMuseum.Business.Controllers;
using IMuseum.Persistence.Repositories.Users;
using IMuseum.Persistence.Models;
using IMuseum.Auth.Services;
using IMuseum.Auth.Authorization;

namespace IMuseum.Auth.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService, IUsersRepository usersRepository)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
    {
        var user = await _userService.Authenticate(model.Username, model.Password);

        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }
}
