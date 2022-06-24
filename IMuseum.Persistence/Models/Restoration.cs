using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

[Table("Restorations")]
public record Restoration : DatabaseModel
{
    public enum RestorationType
    {
        Scientific,
        AestheticFunctional,
        Commercial,
        Other
    }
    public RestorationType Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    [ForeignKey("Artwork")]
    public int ArtworkId { get; set;}
    public Artwork Artwork { get; set; }
}