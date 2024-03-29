using System.ComponentModel.DataAnnotations;

namespace IMuseum.Business.Dtos.Artworks;

public record ArtworkGetParamDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string[]? Rooms { get; set; }
    public string[]? Author { get; set; }
    public string[]? Type { get; set; }
    public string[]? Statuses { get; set; }
    public string? Search { get; set; }
}