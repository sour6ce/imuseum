using IMuseum.Persistence.Models;

namespace IMuseum.Persistence.Repositories;

public class InMemArtworksRepository : IArtworksRepository
{
    private readonly List<Artwork> artworks = new()
    {
        new Artwork { Id = Guid.NewGuid(), Title = "La Gioconda", Author = "Leonardo da Vinci", CreationDate = new DateOnly(1519, 1, 1), IncorporatedDate = DateTime.Now, Period = "Renaissance", Assessment = 10000000 },
        new Artwork { Id = Guid.NewGuid(), Title = "David", Author = "Michelangelo Buonarroti", CreationDate = new DateOnly(1504, 1, 1), IncorporatedDate = DateTime.Now, Period = "Renaissance", Assessment = 7000000 },
        new Artwork { Id = Guid.NewGuid(), Title = "La Nascita di Venere", Author = "Sandro Botticelli", CreationDate = new DateOnly(1510, 1, 1), IncorporatedDate = DateTime.Now, Period = "Renaissance", Assessment = 9000000 },
    };

    public async Task<IEnumerable<Artwork>> GetArtworksAsync()
    {
        return await Task.FromResult(artworks);
    }

    public async Task<Artwork> GetArtworkAsync(Guid id)
    {
        var artwork = artworks.Where(artwork => artwork.Id == id).SingleOrDefault();
        return await Task.FromResult(artwork);
    }

    public async Task CreateArtworkAsync(Artwork artwork)
    {
        artworks.Add(artwork);
        await Task.CompletedTask;
    }

    public async Task UpdateArtworkAsync(Artwork artwork)
    {
        var index = artworks.FindIndex(existingArtwork => existingArtwork.Id == artwork.Id);
        artworks[index] = artwork;
        await Task.CompletedTask;
    }

    public async Task DeleteArtworkAsync(Guid id)
    {
        var index = artworks.FindIndex(existingArtwork => existingArtwork.Id == id);
        artworks.RemoveAt(index);
        await Task.CompletedTask;
    }
}