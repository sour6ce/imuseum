using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Rooms;
using IMuseum.Business.Dtos.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IMuseum.Business.Dtos;
using Microsoft.EntityFrameworkCore;
using IMuseum.Auth.Authorization;
namespace IMuseum.Business.Controllers;

[CatalogManager]
//GET /rooms
[ApiController]
[Route("rooms")]
public class RoomsController : ControllerBase
{
    private readonly IRoomsRepository roomsRepository;

    public RoomsController(IRoomsRepository roomsRepository)
    {
        this.roomsRepository = roomsRepository;
    }

    internal SimpleDto RoomAsDto(Room room)
    {
        return new SimpleDto()
        {
            Id = room.Id,
            Name = room.Name
        };
    }


    //GET /rooms
    [HttpGet]
    public async Task<RoomGetReturnDto> GetRoomsAsync([FromQuery] string? search = "")
    {
        search = search ?? "";

        var filter = (DbSet<Room> x) => x.Where(y => y.Name.ToLower().Contains(search.ToLower()));

        var rooms = roomsRepository.ExecuteOnDbAsync(
            async x => await filter(x).Select(y => RoomAsDto(y)).ToArrayAsync()
        );

        var count = roomsRepository.ExecuteOnDbAsync(
            async x => await filter(x).CountAsync()
        );

        return new RoomGetReturnDto()
        {
            Rooms = await rooms,
            Count = await count
        };
    }

    //GET /rooms/{id}
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<SimpleDto>> GetRoomAsync(int id)
    {
        var room = await roomsRepository.GetObjectAsync(id);

        if (room is null)
        {
            return NotFound();
        }

        return new SimpleDto()
        {
            Id = room.Id,
            Name = room.Name
        };
    }

    //POST /rooms
    [HttpPost]
    public async Task<ActionResult<SimpleDto>> CreateRoomAsync(SimpleNameDto roomDto)
    {
        Room room = new Room()
        {
            Name = roomDto.Name
        };
        await roomsRepository.AddAsync(room);
        return RoomAsDto(room);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteRoom(int id)
    {
        var success = await roomsRepository.RemoveAsync(id);
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
    public async Task<ActionResult> UpdateRoom(int id, SimpleNameDto dto)
    {
        Room room = new Room()
        {
            Name = dto.Name
        };

        var found = await roomsRepository.GetObjectAsync(id);

        if (found == null)
            return NotFound();

        await roomsRepository.UpdateObjectAsync(room);
        return AcceptedAtAction(nameof(UpdateRoom), dto);
    }
}