using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

/// <summary>
/// Represents an user of the system with the roles it has.
/// </summary>
[Table("User")]
public record User : DatabaseModel
{
    /// <summary>
    /// Auto-generated ID for the user.
    /// </summary>
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public Guid UserId { get; set; }
    public string Username { get; set; }
    /// <summary>
    /// Encrypted password of the user
    /// </summary>
    public string Password { get; set; }
    public ICollection<Role> Roles { get; set; }
}