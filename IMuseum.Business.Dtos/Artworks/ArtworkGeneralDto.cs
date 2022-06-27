

namespace IMuseum.Business.Dtos.Artworks;

public record ArtworkGeneralDto : ArtworkPutPostDto
{
    public int Id { get; set; }
    public Persistence.Models.Artwork.ArtworkStatus Status { get; set; }
}