using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

[Table("Image")]
 public record Image : DatabaseModel
 {
    public string Title { get; set; }
    public byte[] Bytes { get; set; }
    public string Description { get; set; }
    public string FileExtension { get; set; }
    public decimal Size { get; set; }
 }