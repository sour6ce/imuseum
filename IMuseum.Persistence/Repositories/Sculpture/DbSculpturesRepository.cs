using Microsoft.EntityFrameworkCore;
using IMuseum.Persistence.Models;
using Microsoft.Extensions.DependencyInjection;

namespace IMuseum.Persistence.Repositories.Sculptures;

public class DbSculpturesRepository : DbRepository<Sculpture>, ISculpturesRepository
{
    public DbSculpturesRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task UpdateObjectAsync(Sculpture item)
    {
#pragma warning disable 8603
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            var old = await iMuseumDbContext.Set<Sculpture>().FirstOrDefaultAsync(old => item.Id == old.Id);
            //Check if actually exists an item with that id
            if (old == null)
            {
                //If not add it
                await this.AddAsync(item);
                return;
            }
            //Code to change each field
            old.Title = item.Title;
            old.Author = item.Author;
            old.CreationDate = item.CreationDate;
            old.IncorporatedDate = item.IncorporatedDate;
            old.Period = item.Period;
            old.Assessment = item.Assessment;
            old.Style = item.Style;
            old.Material = item.Material;

            //Save changes
            await iMuseumDbContext.SaveChangesAsync();
        }
#pragma warning restore 8603
    }
}