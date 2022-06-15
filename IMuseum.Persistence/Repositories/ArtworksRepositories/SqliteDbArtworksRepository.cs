using Microsoft.EntityFrameworkCore;
using IMuseum.Persistence.Models;
using Microsoft.Extensions.DependencyInjection;

namespace IMuseum.Persistence.Repositories;

public class SqliteDbArtworksRepository : IArtworksRepository
{
    private readonly IServiceProvider serviceProvider;

    public SqliteDbArtworksRepository(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task<IEnumerable<Artwork>> GetArtworksAsync()
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            return await iMuseumDbContext.Artworks.ToListAsync();
        }
    }

    public async Task<Artwork> GetArtworkAsync(Guid id)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            return await iMuseumDbContext.Artworks.FirstOrDefaultAsync(artwork => artwork.ArtworkId == id);
        }
    }

    public async Task CreateArtworkAsync(Artwork artwork)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            iMuseumDbContext.Artworks.Add(artwork);
            await iMuseumDbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateArtworkAsync(Artwork artwork)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            var oldArtwork = await iMuseumDbContext.Artworks.FirstOrDefaultAsync(oldArtwork => artwork.ArtworkId == oldArtwork.ArtworkId);
            oldArtwork.Title = artwork.Title;
            oldArtwork.Author = artwork.Author;
            oldArtwork.CreationDate = artwork.CreationDate;
            oldArtwork.AddDate = artwork.AddDate;
            oldArtwork.Period = artwork.Period;
            oldArtwork.Assessment = artwork.Assessment;

            await iMuseumDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteArtworkAsync(Guid id)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            iMuseumDbContext.Artworks.Remove(await iMuseumDbContext.Artworks.FirstOrDefaultAsync(artwork => artwork.ArtworkId == id));
            await iMuseumDbContext.SaveChangesAsync();
        }
    }
}