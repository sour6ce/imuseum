using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

[Table("Rooms")]
public record Room : DatabaseModel
{
    public string Name { get; set; }
    /// <summary>
    /// Artworks on display in te room.
    /// </summary>
    public ICollection<Artwork> Artworks { get; set; }
}