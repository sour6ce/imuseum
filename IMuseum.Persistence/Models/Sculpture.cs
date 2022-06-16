using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

[Table("Sculpture")]
public record Sculpture : PlasticArt
{
    public string Material { get; set; }
}