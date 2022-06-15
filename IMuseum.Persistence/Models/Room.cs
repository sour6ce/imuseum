namespace IMuseum.Persistence.Models;

public record Room
{
    public Guid RoomId { get; set; }
    public string Name { get; set; }

    public List<Artwork> Artworks { get; set; }
}