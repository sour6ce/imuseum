using IMuseum.Persistence.Models;
using IMuseum.Business.Dtos.Artworks;

namespace IMuseum.Business.Dtos.LoanApplications;

public record LoanApplicationPutPostDto
{
    public DateTime ApplicationDate { get; set; }
    public int Duration { get; set; }
    public int ArtworkId { get; set; }
    public ArtworkGeneralDto Artwork { get; set; }
    public int? MuseumId { get; set; }
}