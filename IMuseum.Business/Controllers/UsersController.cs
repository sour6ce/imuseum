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

namespace IMuseum.Business.Controllers;

//GET /users
[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository usersRepository;

    public UsersController(IUsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }

    internal User UserFromDto(UserPostAndPutDto dto)
    {
        return new User(){
            Username = dto.Username,
            Email = dto.Email,
            RoleId = dto.RoleId,
            Password = (new Guid()).ToString()
        };
    }

    internal UserPostAndPutDto UserAsDto(User user)
    {
        return new UserPostAndPutDto(){
            Username = user.Username,
            Email = user.Email,
            RoleId = user.RoleId
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
            .Take(args.PageSize).ToArray();
        }));
        return new UserGetReturnDto()
        {
            Users = (users).Select((x) => this.UserAsDto(x)).ToArray(),
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
        
        user.RoleId = dto.RoleId;
        user.Username = dto.Username;
        user.Email = dto.Email;
        await usersRepository.UpdateObjectAsync(user);
        return dto;
    }
}