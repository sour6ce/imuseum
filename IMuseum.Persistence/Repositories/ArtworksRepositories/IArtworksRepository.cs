using IMuseum.Persistence.Models;

namespace IMuseum.Persistence.Repositories;

public interface IArtworksRepository
{
    Task<Artwork> GetArtworkAsync(Guid id);
    Task<IEnumerable<Artwork>> GetArtworksAsync();
    Task CreateArtworkAsync(Artwork artwork);
    Task UpdateArtworkAsync(Artwork artwork);
    Task DeleteArtworkAsync(Guid id);
}