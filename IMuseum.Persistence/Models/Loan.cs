using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

/// <summary>
/// Represents an approved loan.
/// </summary>
[Table("Loans")]
public record Loan : DatabaseModel
{
    /// <summary>
    /// Date and Time the application was accepted and the loan
    /// actually started.
    /// </summary>
    public DateTime StartDate { get; set; }
    public decimal PaymentAmount { get; set; }
    /// <summary>
    /// Aplication related to this loan.
    /// </summary>
    public LoanApplication Application { get; set; }
}