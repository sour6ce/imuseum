using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

/// <summary>
/// Represents a request of an artwork for a loan.
/// </summary>
[Table("LoanApplications")]
public record LoanApplication : DatabaseModel
{
    // FIXME: Delete Id field
    // FIXME: Delete Name field
    /// <summary>
    /// Auto-generated ID for the loan application.
    /// </summary>
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public Guid LoanApplicationId { get; set; }
    public string Name { get; set; }
    /// <summary>
    /// Date the application for a loan was sended.
    /// </summary>
    public DateTime ApplicationDate { get; set; }
    public int Duration { get; set; }
    public LoanStatus Status { get; set; } = LoanStatus.OnWait;
    public Artwork Artwork { get; set; }
    /// <summary>
    /// Related museum to the loan. In case of internal artworks
    /// stores the museum that request the artwork. For external
    /// artworks should be the museum taht owns the artwork and then
    /// march to the museum pointed in the Artwork data.
    /// </summary>
    public Museum RelatedMuseum { get; set; }
}