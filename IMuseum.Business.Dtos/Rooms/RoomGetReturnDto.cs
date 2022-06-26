using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos.Rooms;

public record RoomGetReturnDto
{
    public Room[] Rooms { get; set; }
    public int Count { get; set; }
}