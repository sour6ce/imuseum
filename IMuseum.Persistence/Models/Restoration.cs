namespace IMuseum.Persistence.Models;

public record Restoration
{
    public Guid RestorationId { get; set; }
    public string Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ArtworkInPosess Artwork { get; set; }
}