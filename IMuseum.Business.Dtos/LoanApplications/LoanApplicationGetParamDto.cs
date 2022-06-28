using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos.LoanApplications;

public record LoanApplicationGetParamDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int? ArtworkId { get; set; }
    public int? MuseumId { get; set; }
    public string? Status { get; set; }
}