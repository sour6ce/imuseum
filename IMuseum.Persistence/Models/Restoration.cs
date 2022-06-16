using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

[Table("Restorations")]
public record Restoration : DatabaseModel
{
    // FIXME: Delete Id field
    /// <summary>
    /// Auto-generated ID for the restoration.
    /// </summary>
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public Guid RestorationId { get; set; }
    public string Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Artwork Artwork { get; set; }
}