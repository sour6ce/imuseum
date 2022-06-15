using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

/// <summary>
/// Represents an external museum. For reference to this museum usually
/// a <c>null</c> value is used.
/// </summary>
[Table("Museums")]
public record Museum : DatabaseModel
{
    /// <summary>
    /// Auto-generated ID for the museum.
    /// </summary>
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public Guid MuseumId { get; set; }
    public string Name { get; set; }
}