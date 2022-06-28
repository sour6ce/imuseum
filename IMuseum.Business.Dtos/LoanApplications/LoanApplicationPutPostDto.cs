using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos.LoanApplications;

public record LoanApplicationPutPostDto
{
    public DateTime ApplicationDate { get; set; }
    public int Duration { get; set; }
    public int ArtworkId { get; set; }
    public int? MuseumId { get; set; }
}