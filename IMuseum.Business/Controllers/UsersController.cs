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

    // TODO: [Hacer el Post de los users. Usar el Dto de UserPostAndPutDto, es trivial aplicando los mismos pasos del Post de los demas controladores]
    // TODO: [Hacer el Put de los users guiandose por el Put de los demas controladores, usar el mismo Dto de UserPostAndPutDto]
}