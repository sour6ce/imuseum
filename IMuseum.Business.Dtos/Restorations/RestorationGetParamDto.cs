using IMuseum.Business.Dtos.Artworks;

namespace IMuseum.Business.Dtos.Restorations;

public record RestorationGetParamDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int[]? ArtworksIds { get; set; }
    public DateTime? StartDateA { get; set; }
    public DateTime? StartDateB { get; set; }
    public DateTime? EndDateA { get; set; }
    public DateTime? EndDateB { get; set; }
}