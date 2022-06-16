using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

[Table("Paintings")]
public record Painting : PlasticArt
{
    // FIXME: Change to property
    public string Media;
}