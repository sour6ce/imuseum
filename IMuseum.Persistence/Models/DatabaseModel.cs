using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

public record DatabaseModel
{
    public Guid Id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime AddTime { get; set; } = DateTime.Now;
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? UpdateTime { get; set; }
    public Boolean Deleted { get; set; } = false;
    public DateTime? DeletedTime { get; set; } = null;
}