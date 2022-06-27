using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Museums;
using IMuseum.Business.Dtos.Museums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IMuseum.Auth.Authorization;

namespace IMuseum.Business.Controllers;


//GET /museums
[Director]
[ApiController]
[Route("museums")]
public class MuseumsController : ControllerBase
{
    private readonly IMuseumsRepository museumsRepository;

    public MuseumsController(IMuseumsRepository museumsRepository)
    {
        this.museumsRepository = museumsRepository;
    }

    internal async Task<MuseumDto> MuseumAsDto(Museum museum)
    {
        return new MuseumDto(){
            Name = museum.Name
        };
    }

    internal async Task<Museum> MuseumFromDto(MuseumDto dto)
    {
        return new Museum(){
            Name = dto.Name
        };
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
            Museums = (museums).Select((x) => this.MuseumAsDto(x).Result).ToArray(),
            Count = (await count)
        };
    }

    //GET /museums
    [HttpGet]
    [Route("{id}")]
    public async Task<MuseumDto> GetMuseumAsync(int id)
    {
        var museum = await museumsRepository.GetObjectAsync(id);

        if(museum is null)
        {
            return new MuseumDto(){
                Name = null
            };
        }

        return new MuseumDto()
        {
            Name = museum.Name
        };
    }

    //POST /museums
    [HttpPost]
    public async Task<ActionResult<MuseumDto>> CreateMuseumAsync(MuseumDto museumDto)
    {
        Museum museum = new Museum(){
            Name = museumDto.Name
        };
        await museumsRepository.AddAsync(museum);
        return museumDto;
    }

    [HttpDelete]
    [Route("{id}")]
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
    [Route("{id}")]
    public async Task<ActionResult> UpdateMuseum(int id, MuseumDto dto)
    {
        Museum museum = new Museum(){
            Name = dto.Name
        };

        var found = await museumsRepository.GetObjectAsync(id);

        if (found == null)
            return NotFound();
        
        await museumsRepository.UpdateObjectAsync(museum);
        return AcceptedAtAction(nameof(UpdateMuseum), dto);
    }
}