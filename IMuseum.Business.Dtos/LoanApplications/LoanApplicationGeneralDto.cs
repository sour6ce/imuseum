using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos.LoanApplications;

public record LoanApplicationGeneralDto : LoanApplicationPutPostDto
{
    public int Id { get; set; }
    public string LoanApplicationStatus { get; set; }
}