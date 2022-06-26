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
    public async Task<MuseumGetReturnDto> GetMuseumsAsync()
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

    //POST /museums
    [HttpPost]
    public async Task<ActionResult<Museum>> CreateMuseumAsync(MuseumParamDto museumDto)
    {
        Museum museum = new Museum(){
            Name = museumDto.Name
        };
        await museumsRepository.AddAsync(museum);
        return museum;
    }

    [HttpDelete]
    [Route("/museums/{id}")]
    public async Task<ActionResult> DeleteMuseum(int id)
    {
        var success = await museumsRepository.RemoveAsync(id);
        if (!success)
        {
            return NotFound();
        }
        else
        {
            return new OkResult();
        }
    }

    [HttpPut]
    [Route("/museums/{id}")]
    public async Task<ActionResult> UpdateMuseum(int id, MuseumParamDto dto)
    {
        Museum museum = new Museum(){
            Name = dto.Name
        };

        var found = await museumsRepository.GetObjectAsync(id);

        if (found == null)
            return NotFound();
        
        await museumsRepository.UpdateObjectAsync(museum);
        return AcceptedAtAction(nameof(UpdateMuseum), museum);
    }
}