using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos.Museums;

public record MuseumGetReturnDto
{
    public MuseumDto[] Museums { get; set; }
    public int Count { get; set; }
}