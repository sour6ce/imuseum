

namespace IMuseum.Business.Dtos.Artworks;

public record ArtworkPutPostDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? IncorporatedDate { get; set; }
    public string Period { get; set; }
    public decimal Assessment { get; set; }
    public string Image { get; set; }
    public ArtworkType Type { get; set; }
    public string? Style { get; set; }
    public string? Media { get; set; }
    public string? Material { get; set; }
}