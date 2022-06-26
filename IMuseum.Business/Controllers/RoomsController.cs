using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Rooms;
using IMuseum.Business.Dtos.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace IMuseum.Business.Controllers;

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

    //GET /rooms
    [HttpGet]
    public async Task<RoomGetReturnDto> GetRoomsAsync()
    {
        var filtered = (DbSet<Room> all) =>
        {
            return all;
        };
        var count = (roomsRepository.ExecuteOnDbAsync(async (all) =>
        {
            return
            await filtered(all).CountAsync();
        }));
        var rooms = (roomsRepository.ExecuteOnDb((all) =>
        {
            return all;
        }));
        return new RoomGetReturnDto()
        {
            Rooms = rooms.ToArray(),
            Count = (await count)
        };
    }

    //POST /rooms
    [HttpPost]
    public async Task<ActionResult<Room>> CreateRoomAsync(RoomParamDto roomDto)
    {
        Room room = new Room(){
            Name = roomDto.Name
        };
        await roomsRepository.AddAsync(room);
        return room;
    }

    [HttpDelete]
    [Route("/rooms/{id}")]
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
    [Route("/rooms/{id}")]
    public async Task<ActionResult> UpdateRoom(int id, RoomParamDto dto)
    {
        Room room = new Room(){
            Name = dto.Name
        };

        var found = await roomsRepository.GetObjectAsync(id);

        if (found == null)
            return NotFound();
        
        await roomsRepository.UpdateObjectAsync(room);
        return AcceptedAtAction(nameof(UpdateRoom), room);
    }
}