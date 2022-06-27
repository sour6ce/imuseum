namespace IMuseum.Business.Dtos.Museums;

public record MuseumGeneralDto : MuseumPutPostDto
{
    public int Id { get; set; }
}