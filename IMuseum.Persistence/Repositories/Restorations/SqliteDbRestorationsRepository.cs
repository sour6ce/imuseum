using Microsoft.EntityFrameworkCore;
using IMuseum.Persistence.Models;
using Microsoft.Extensions.DependencyInjection;

namespace IMuseum.Persistence.Repositories.Restorations;

public class SqliteDbRestorationsRepository : SqliteDbRepository<Restoration>, IRestorationsRepository
{
    public SqliteDbRestorationsRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public async Task UpdateRestorationAsync(Restoration item)
    {
#pragma warning disable 8603
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
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