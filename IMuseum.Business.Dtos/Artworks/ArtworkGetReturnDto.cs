using System.ComponentModel.DataAnnotations;

namespace IMuseum.Business.Dtos.Artworks;

public record ArtworkGetReturnDto
{
    public ArtworkGeneralDto[] Artworks { get; set; }
    public int Count { get; set; }
}