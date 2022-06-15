namespace IMuseum.Persistence.Models;

public record LoanApplication
{
    public Guid LoanApplicationId { get; set; }
    public string Name { get; set; }
    public DateTime ApplicationDate { get; set; }
    public int Duration { get; set; }
    public string Status { get; set; }
    public ArtworkInPosess Artwork { get; set; }
    public FriendMuseum Museum { get; set; }
}