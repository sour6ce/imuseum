using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

/// <summary>
/// Represents a request of an artwork for a loan.
/// </summary>
[Table("LoanApplications")]
public record LoanApplication : DatabaseModel
{
    /// <summary>
    /// Status of an loan application
    /// </summary>
    public enum LoanApplicationStatus
    {
        /// <summary>
        /// The artwork was requested but not yet approved
        /// </summary>
        OnWait,
        /// <summary>
        /// The loan was approved and the artwork is curently
        /// on loan
        /// </summary>
        OnLoan,
        /// <summary>
        /// The application was approved and the loan time 
        /// finished
        /// </summary>
        Finished,
        /// <summary>
        /// The application was denied.
        /// </summary>
        Denied,
    }
    /// <summary>
    /// Date the application for a loan was sended.
    /// </summary>
    public DateTime ApplicationDate { get; set; }
    public int Duration { get; set; }
    public LoanApplicationStatus CurrentStatus { get; set; } = LoanApplicationStatus.OnWait;
    [ForeignKey("Artwork")]
    public int ArtworkId { get; set; } = 0;
    public Artwork? Artwork { get; set; }

    /// <summary>
    /// Related museum to the loan. In case of internal artworks
    /// stores the museum that request the artwork. For external
    /// artworks should be the museum taht owns the artwork and then
    /// march to the museum pointed in the Artwork data.
    /// </summary>
    [ForeignKey("Museum")]
    public int? MuseumId { get; set; } = 0;
    public Museum? RelatedMuseum { get; set; }

    /// <summary>
    /// Loan details. Null if they haven't been accepted. (Related from relation to Loans)
    /// </summary>
    [ForeignKey("Loan")]
    public int LoanId { get; set; }
    public Loan? LoanRelated { get; set; }
}