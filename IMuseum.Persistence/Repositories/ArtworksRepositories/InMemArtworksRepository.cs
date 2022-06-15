using IMuseum.Persistence.Models;

namespace IMuseum.Persistence.Repositories;

public class InMemArtworksRepository : IArtworksRepository
{
    private readonly List<Artwork> artworks = new()
    {
        new Artwork { ArtworkId = Guid.NewGuid(), Title = "La Gioconda", Author = "Leonardo da Vinci", CreationDate = new DateTime(1519, 1, 1), AddDate = DateTime.Now, Period = "Renaissance", Assessment = 10000000 },
        new Artwork { ArtworkId = Guid.NewGuid(), Title = "David", Author = "Michelangelo Buonarroti", CreationDate = new DateTime(1504, 1, 1), AddDate = DateTime.Now, Period = "Renaissance", Assessment = 7000000 },
        new Artwork { ArtworkId = Guid.NewGuid(), Title = "La Nascita di Venere", Author = "Sandro Botticelli", CreationDate = new DateTime(1510, 1, 1), AddDate = DateTime.Now, Period = "Renaissance", Assessment = 9000000 },
    };

    public async Task<IEnumerable<Artwork>> GetArtworksAsync()
    {
        return await Task.FromResult(artworks);
    }

    public async Task<Artwork> GetArtworkAsync(Guid id)
    {
        var artwork = artworks.Where(artwork => artwork.ArtworkId == id).SingleOrDefault();
        return await Task.FromResult(artwork);
    }

    public async Task CreateArtworkAsync(Artwork artwork)
    {
        artworks.Add(artwork);
        await Task.CompletedTask;
    }

    public async Task UpdateArtworkAsync(Artwork artwork)
    {
        var index = artworks.FindIndex(existingArtwork => existingArtwork.ArtworkId == artwork.ArtworkId);
        artworks[index] = artwork;
        await Task.CompletedTask;
    }

    public async Task DeleteArtworkAsync(Guid id)
    {
        var index = artworks.FindIndex(existingArtwork => existingArtwork.ArtworkId == id);
        artworks.RemoveAt(index);
        await Task.CompletedTask;
    }
}