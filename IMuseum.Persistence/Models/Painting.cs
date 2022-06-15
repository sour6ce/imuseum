using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

[Table("Paintings")]
public record Painting : PlasticArt
{
    public string Media;
}