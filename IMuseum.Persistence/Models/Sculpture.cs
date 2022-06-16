using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

[Table("Sculpture")]
public record Sculpture : PlasticArt
{
    // FIXME: Change to property
    public string Material;
}