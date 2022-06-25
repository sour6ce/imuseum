using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos.Artists;

public record ArtistsGetReturnDto
{
    public string[] Artists { get; set; }
    public int Count { get; set; }
}