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
    public enum Status
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
    public Status CurrentStatus { get; set; } = Status.OnWait;
    [ForeignKey("Artwork")]
    public Guid ArtworkId { get; set;}
    public Artwork Artwork { get; set; }

    /// <summary>
    /// Related museum to the loan. In case of internal artworks
    /// stores the museum that request the artwork. For external
    /// artworks should be the museum taht owns the artwork and then
    /// march to the museum pointed in the Artwork data.
    /// </summary>
    [ForeignKey("Museum")]
    public Guid MuseumId{ get; set;}
    public Museum RelatedMuseum { get; set; }

    /// <summary>
    /// Loan details. Null if they haven't been accepted. (Related from relation to Loans)
    /// </summary>
    [ForeignKey("Loan")]
    public Guid LoanId{get;set;}
    public Loan? LoanRelated { get; set; }
}