using System.ComponentModel.DataAnnotations;

namespace IMuseum.Business.Dtos.Artworks;

public record ArtworkGetParamDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string[] Author { get; set; }
    public ArtworkType[] Type { get; set; }
    public Persistence.Models.Artwork.Status[] Statuses { get; set; }
    public string? Search { get; set; }
}