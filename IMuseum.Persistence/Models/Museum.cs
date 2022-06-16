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
}