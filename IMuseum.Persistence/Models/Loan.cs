namespace IMuseum.Persistence.Models;

public record Loan
{
    public Guid LoanId { get; set; }
    public DateTime ApplicationDate { get; set; }
    public decimal PaymentAmount { get; set; }
    public LoanApplication Application { get; set; }
}