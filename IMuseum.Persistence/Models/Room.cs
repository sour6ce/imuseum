using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

[Table("Rooms")]
public record Room : DatabaseModel
{
    // FIXME: Delete Id field
    /// <summary>
    /// Auto-generated ID for the room.
    /// </summary>
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public Guid RoomId { get; set; }
    public string Name { get; set; }
    /// <summary>
    /// Artworks on display in te room.
    /// </summary>
    public ICollection<Artwork> Artworks { get; set; }
}