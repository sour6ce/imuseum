using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Museums;
using IMuseum.Business.Dtos.Museums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace IMuseum.Business.Controllers;

//GET /museums
[ApiController]
[Route("museums")]
public class MuseumsController : ControllerBase
{
    private readonly IMuseumsRepository museumsRepository;

    public MuseumsController(IMuseumsRepository museumsRepository)
    {
        this.museumsRepository = museumsRepository;
    }

    //GET /museums
    [HttpGet]
    public async Task<MuseumGetReturnDto> GetUsersAsync()
    {
        var filtered = (DbSet<Museum> all) =>
        {
            return all;
        };
        var count = (museumsRepository.ExecuteOnDbAsync(async (all) =>
        {
            return
            await filtered(all).CountAsync();
        }));
        var museums = (museumsRepository.ExecuteOnDb((all) =>
        {
            return all;
        }));
        return new MuseumGetReturnDto()
        {
            Museums = museums.ToArray(),
            Count = (await count)
        };
    }
}