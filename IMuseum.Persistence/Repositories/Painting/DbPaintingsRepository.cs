using Microsoft.EntityFrameworkCore;
using IMuseum.Persistence.Models;
using Microsoft.Extensions.DependencyInjection;

namespace IMuseum.Persistence.Repositories.Paintings;

public class DbPaintingsRepository : DbRepository<Painting>, IPaintingsRepository
{
    public DbPaintingsRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task UpdateObjectAsync(Painting item)
    {
#pragma warning disable 8603
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            var old = await iMuseumDbContext.Set<Painting>().FirstOrDefaultAsync(old => item.Id == old.Id);
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
            old.Media = item.Media;
            old.Image = item.Image;
            old.Style = item.Style;

            //Save changes
            await iMuseumDbContext.SaveChangesAsync();
        }
#pragma warning restore 8603
    }
}