using Microsoft.EntityFrameworkCore;
using IMuseum.Persistence.Models;
using Microsoft.Extensions.DependencyInjection;

namespace IMuseum.Persistence.Repositories.Museums;

public class SqliteDbMuseumsRepository : SqliteDbRepository<Museum>, IMuseumsRepository
{
    public SqliteDbMuseumsRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task UpdateObjectAsync(Museum item)
    {
#pragma warning disable 8603
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            var old = await iMuseumDbContext.Museums.FirstOrDefaultAsync(old => item.Id == old.Id);
            //Check if actually exists an item with that id
            if (old == null)
            {
                //If not add it
                await this.AddAsync(item);
                return;
            }
            //Code to change each field
            old.Name = item.Name;

            //Save changes
            await iMuseumDbContext.SaveChangesAsync();
        }
#pragma warning restore 8603
    }
}