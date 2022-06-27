using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Rooms;
using IMuseum.Business.Dtos.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

    internal RoomGeneralDto RoomAsDto(Room room)
    {
        return new RoomGeneralDto()
        {
            Id = room.Id,
            Name = room.Name
        };
    }


    //GET /rooms
    [HttpGet]
    public async Task<RoomGetReturnDto> GetRoomsAsync()
    {
        return new RoomGetReturnDto()
        {
            Rooms = (await roomsRepository.GetObjectsAsync()).Select((x) => RoomAsDto(x)).ToArray(),
            Count = roomsRepository.Count
        };
    }

    //GET /museums
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<RoomGeneralDto>> GetRoomAsync(int id)
    {
        var room = await roomsRepository.GetObjectAsync(id);

        if (room is null)
        {
            return NotFound();
        }

        return new RoomGeneralDto()
        {
            Id = room.Id,
            Name = room.Name
        };
    }

    //POST /rooms
    [HttpPost]
    public async Task<ActionResult<RoomGeneralDto>> CreateRoomAsync(RoomPutPostDto roomDto)
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
    public async Task<ActionResult> UpdateRoom(int id, RoomPutPostDto dto)
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