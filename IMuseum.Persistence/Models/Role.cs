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
    public string Name { get; set; }
}