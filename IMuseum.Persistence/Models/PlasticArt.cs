using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;


[Table("PlasticArt")]
public record PlasticArt : Artwork
{
    public string Style { get; set; }
}