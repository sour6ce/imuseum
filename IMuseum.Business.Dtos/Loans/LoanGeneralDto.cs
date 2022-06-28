using IMuseum.Persistence.Models;
using IMuseum.Business.Dtos.LoanApplications;

namespace IMuseum.Business.Dtos.Loans;

public record LoanGeneralDto
{
    public DateTime StartDate { get; set; }
    public decimal PaymentAmount { get; set; }
    public int LoanApplicationId { get; set; }
    public LoanApplicationGeneralDto LoanApplication {get;set;}
}

