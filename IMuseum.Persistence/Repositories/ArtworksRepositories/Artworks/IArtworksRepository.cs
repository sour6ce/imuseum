using IMuseum.Persistence.Models;

namespace IMuseum.Persistence.Repositories.Artworks;

public interface IArtworksRepository : IRepository<Artwork>
{
    Task UpdateArtworkAsync(Artwork artwork);
}