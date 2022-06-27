
using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Artworks;
using IMuseum.Persistence.Repositories.Paintings;
using IMuseum.Persistence.Repositories.Sculptures;
using IMuseum.Persistence.Repositories.Restorations;
using IMuseum.Persistence.Repositories.Users;
using IMuseum.Business.Dtos.Artworks;
using IMuseum.Business.Dtos.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IMuseum.Persistence.Services;
using IMuseum.Auth.Authorization;

namespace IMuseum.Business.Controllers;

//GET /users
[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUsersRepository usersRepository;

    public UsersController(IUserService userService, IUsersRepository usersRepository)
    {
        this._userService = userService;
        this.usersRepository = usersRepository;
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

    internal User UserFromDto(UserPostAndPutDto dto)
    {
        return new User(){
            Username = dto.FirstName + " " + dto.LastName,
            Email = dto.Email,
            Roles = dto.Roles,
            Password = (new Guid()).ToString()
        };
    }

    //GET /users
    [HttpGet]
    public async Task<UserGetReturnDto> GetUsersAsync(UserGetParamDto args)
    {
        var filtered = (DbSet<User> all) =>
        {
            return all;
        };
        var count = (usersRepository.ExecuteOnDbAsync(async (all) =>
        {
            return
            await filtered(all).CountAsync();
        }));
        var users = (usersRepository.ExecuteOnDb((all) =>
        {
            return
            filtered(all).Skip(args.PageSize * (args.Page - 1))
            .Take(args.PageSize);
        }));
        return new UserGetReturnDto()
        {
            Users = users.ToArray(),
            Count = (await count)
        };
    }

    //POST /users
    [HttpPost]
    public async Task<ActionResult<UserPostAndPutDto>> CreateUserAsync(UserPostAndPutDto userDto)
    {
        var user = UserFromDto(userDto);
        await usersRepository.AddAsync(user);
        return CreatedAtAction(nameof(CreateUserAsync), new { Id = user.Id }, userDto);
    }

    //PUT /users/[id]
    [HttpPut]
    [Route("{id}")]
    public async Task<UserPostAndPutDto> UpdateUserAsync(int id, UserPostAndPutDto dto)
    {
        User user = await usersRepository.GetObjectAsync(id);
        if(user is null)
            return null;
        
        user.Roles = dto.Roles;
        user.Username = dto.FirstName + " " + dto.LastName;
        user.Email = dto.Email;
        await usersRepository.UpdateObjectAsync(user);
        return dto;
    }
}
