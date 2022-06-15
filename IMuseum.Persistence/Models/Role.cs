using System.ComponentModel.DataAnnotations.Schema;
using IMuseum.Persistence.Models;

namespace IMuseum.Persistence.Models;

/// <summary>
/// Represents the role of a user in the system. It's related
/// to permissions to do some actions.
/// </summary>
[Table("Roles")]
public record Role : DatabaseModel
{
    /// <summary>
    /// Auto-generated ID for the role.
    /// </summary>
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public Guid RoleId { get; set; }
    public string Account { get; set; }
}