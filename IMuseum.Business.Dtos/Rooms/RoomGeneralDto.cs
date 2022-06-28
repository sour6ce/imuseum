namespace IMuseum.Business.Dtos.Rooms;

public record RoomGeneralDto : RoomPutPostDto
{
    public int Id { get; set; }
}