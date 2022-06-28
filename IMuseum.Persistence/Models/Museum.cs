using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

/// <summary>
/// Represents an external museum. For reference to this museum usually
/// a <c>null</c> value is used.
/// </summary>
[Table("Museums")]
public record Museum : DatabaseModel
{
    public string Name { get; set; }
    /// <summary>
    /// Artworks of this museum. (Related from relation to Artworks)
    /// </summary>
    public ICollection<Artwork> Artworks { get; set; }
    /// <summary>
    /// All loan applications of this museum. (Related from relation to Loan Applications)
    /// </summary>
    public ICollection<LoanApplication> LoanApplications { get; set; }
}