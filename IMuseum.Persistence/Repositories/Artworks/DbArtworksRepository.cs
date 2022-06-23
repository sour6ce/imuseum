using Microsoft.EntityFrameworkCore;
using IMuseum.Persistence.Models;
using Microsoft.Extensions.DependencyInjection;

namespace IMuseum.Persistence.Repositories.Artworks;

public class DbArtworksRepository : DbRepository<Artwork>, IArtworksRepository
{
    public DbArtworksRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task UpdateObjectAsync(Artwork artwork)
    {
#pragma warning disable 8603
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            var oldArtwork = await iMuseumDbContext.Set<Artwork>().FirstOrDefaultAsync(oldArtwork => artwork.Id == oldArtwork.Id);
            if (oldArtwork == null)
            {
                await this.AddAsync(artwork);
                return;
            }
            oldArtwork.Title = artwork.Title;
            oldArtwork.Author = artwork.Author;
            oldArtwork.CreationDate = artwork.CreationDate;
            oldArtwork.IncorporatedDate = artwork.IncorporatedDate;
            oldArtwork.Period = artwork.Period;
            oldArtwork.Assessment = artwork.Assessment;
            oldArtwork.Description = artwork.Description;
            oldArtwork.CurrentSatus = artwork.CurrentSatus;

            await iMuseumDbContext.SaveChangesAsync();
        }
#pragma warning restore 8603
    }
}