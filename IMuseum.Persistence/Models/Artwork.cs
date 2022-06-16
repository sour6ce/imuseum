using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

[Table("Artworks")]
public record Artwork : DatabaseModel
{
    public string Title { get; set; }
    public string Author { get; set; }
    /// <summary>
    /// Date the artwork was created by the artist.
    /// </summary>
    public DateTime CreationDate { get; set; }
    /// <summary>
    /// Date and Time the artwork was incorporated to the Museum. In case
    /// of an external artwork this can be the time it arrives to the museum.
    /// Has <c>null</c> if the artwork is registered but not yet in the museum.
    /// </summary>
    public DateTime? IncorporatedDate { get; set; } = DateTime.Now;
    /// <summary>
    /// Artistic period of the artwork.
    /// </summary>
    public string Period { get; set; }
    public decimal Assessment { get; set; }
    /// <summary>
    /// Status of the artwork inside or outside the museum.
    /// </summary>
    public ArtworkStatus Status { get; set; } = ArtworkStatus.InStorage;
    /// <summary>
    /// The museum owner of the artwork, in case of an internal artwork this
    /// field must be <c>null</c>.
    /// </summary>
    public Museum? Museum { get; set; } = null;
}