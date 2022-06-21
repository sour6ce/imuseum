using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

[Table("Image")]
public record Image : DatabaseModel
{
    public string Title { get; set; }
    public byte[] Bytes { get; set; }
    public string FileExtension { get; set; }
    public long Size { get; set; }
    /// <summary>
    /// Artwork of this image. (Related from relation to artworks)
    /// </summary>
    public Artwork Artwork { get; set; }
}