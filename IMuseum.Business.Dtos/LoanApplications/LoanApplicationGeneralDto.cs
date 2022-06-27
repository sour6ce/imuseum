using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos.LoanApplications;

public record LoanApplicationGeneralDto
{
    public int? Id { get; set; } 
    public DateTime? ApplicationDate { get; set; }
    public int? Duration { get; set; }
    public LoanApplication.LoanApplicationStatus? LoanApplicationStatus { get; set; }
    public int? ArtworkId { get; set; }
    public int? MuseumId { get; set; }
}