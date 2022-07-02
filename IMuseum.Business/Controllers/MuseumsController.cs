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

    internal MuseumGeneralDto MuseumAsDto(Museum museum)
    {
        return new MuseumGeneralDto()
        {
            Id = museum.Id,
            Name = museum.Name
        };
    }

    //GET /museums
    [HttpGet]
    public async Task<MuseumGetReturnDto> GetMuseumsAsync([FromQuery] string search = "")
    {
        return new MuseumGetReturnDto()
        {
            Museums = (await museumsRepository.ExecuteOnDbAsync(
                async x => await x.Where(y => y.Name.ToLower().Contains(search.ToLower()))
                .Select(y => MuseumAsDto(y)).ToArrayAsync())
                ),
            Count = museumsRepository.Count
        };
    }

    //GET /museums
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<MuseumGeneralDto>> GetMuseumAsync(int id)
    {
        var museum = await museumsRepository.GetObjectAsync(id);

        if (museum is null)
        {
            return NotFound();
        }

        return MuseumAsDto(museum);
    }

    //POST /museums
    [HttpPost]
    public async Task<ActionResult<MuseumPutPostDto>> CreateMuseumAsync(MuseumPutPostDto museumDto)
    {
        Museum museum = new Museum()
        {
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
    public async Task<ActionResult> UpdateMuseum(int id, MuseumPutPostDto dto)
    {
        Museum museum = new Museum()
        {
            Name = dto.Name
        };

        var found = await museumsRepository.GetObjectAsync(id);

        if (found == null)
            return NotFound();

        await museumsRepository.UpdateObjectAsync(museum);
        return AcceptedAtAction(nameof(UpdateMuseum), dto);
    }
}