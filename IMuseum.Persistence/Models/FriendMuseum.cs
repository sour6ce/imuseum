namespace IMuseum.Persistence.Models;

public record FriendMuseum
{
    public Guid FriendMuseumId { get; set; }
    public string Name { get; set; }
}