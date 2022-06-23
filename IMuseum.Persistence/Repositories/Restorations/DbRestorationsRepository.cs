using Microsoft.EntityFrameworkCore;
using IMuseum.Persistence.Models;
using Microsoft.Extensions.DependencyInjection;

namespace IMuseum.Persistence.Repositories.Restorations;

public class DbRestorationsRepository : DbRepository<Restoration>, IRestorationsRepository
{
    public DbRestorationsRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task UpdateObjectAsync(Restoration item)
    {
#pragma warning disable 8603
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
            var old = await iMuseumDbContext.Set<Restoration>().FirstOrDefaultAsync(old => item.Id == old.Id);
            //Check if actually exists an item with that id
            if (old == null)
            {
                //If not add it
                await this.AddAsync(item);
                return;
            }
            //Code to change each field
            old.Artwork = item.Artwork;
            old.EndDate = item.EndDate;
            old.StartDate = item.StartDate;
            old.Type = item.Type;

            //Save changes
            await iMuseumDbContext.SaveChangesAsync();
        }
#pragma warning restore 8603
    }
}