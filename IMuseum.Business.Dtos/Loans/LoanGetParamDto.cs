using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos.Loans;

public record LoanGetParamDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int? ArtworkId { get; set; }
    public int? MuseumId { get; set; }
    public int? IncomeMin { get; set; }
    public int? IncomeMax { get; set; }
}