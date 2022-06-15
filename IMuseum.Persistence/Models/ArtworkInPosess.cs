namespace IMuseum.Persistence.Models;

public enum ArtworkStatus
{
    OnLoan,
    OnRestoration,
    OnPosess
}

public record ArtworkInPosess : Artwork
{
    public ArtworkStatus Status { get; set; }
}