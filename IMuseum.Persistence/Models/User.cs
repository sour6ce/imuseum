namespace IMuseum.Persistence.Models;

public enum UserRole
{
    Visitor,
    CatalogKeeper,
    ChiefRestaurateur,
    MuseumDirector
}

public record User
{
    public Guid UserId { get; set; }
    public string Account { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
}