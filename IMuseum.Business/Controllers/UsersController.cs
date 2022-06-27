
using Microsoft.AspNetCore.Mvc;
using IMuseum.Business.Controllers;
using IMuseum.Persistence.Repositories.Users;
using IMuseum.Persistence.Models;
using IMuseum.Persistence.Services;
using IMuseum.Auth.Authorization;

namespace IMuseum.Auth.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;
    private IUsersRepository _usersRepository;

    public UsersController(IUserService userService, IUsersRepository usersRepository)
    {
        _userService = userService;
        _usersRepository = usersRepository;
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
        var users = _usersRepository.GetObjects();
        return Ok(users);
    }
}
