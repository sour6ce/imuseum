

namespace IMuseum.Business.Dtos.Artworks;

public record ArtworkGeneralDto : ArtworkPutPostDto
{
    public int Id { get; set; }
    public string Status { get; set; }
    public string Room { get; set; }
}