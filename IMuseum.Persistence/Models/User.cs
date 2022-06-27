using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

/// <summary>
/// Represents an user of the system with the roles it has.
/// </summary>
[Table("User")]
public record User : DatabaseModel
{
    public string Username { get; set; }
    /// <summary>
    /// Encrypted password of the user
    /// </summary>
    public string Password { get; set; }
    public string Email { get; set; }

    [ForeignKey("Role")]
    public int RoleId {get;set;}
    public Role Role { get; set; }
}