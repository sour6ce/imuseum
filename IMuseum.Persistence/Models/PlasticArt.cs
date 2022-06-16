using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;


[Table("PlasticArt")]
public record PlasticArt : Artwork
{
    // FIXME: Change to property
    public string Style;
}