using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos.Loans;

public record LoanGeneralDto
{
    public decimal PaymentAmount { get; set; }
    public int LoanApplicationId { get; set;}
}

    