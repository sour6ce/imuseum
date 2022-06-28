using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos.Loans;

public record LoanGeneralDto
{
    public DateTime StartDate { get; set; }
    public decimal PaymentAmount { get; set; }
    public int LoanApplicationId { get; set; }
}

