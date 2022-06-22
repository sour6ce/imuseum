using Microsoft.EntityFrameworkCore;
using IMuseum.Persistence.Models;
using Microsoft.Extensions.DependencyInjection;

namespace IMuseum.Persistence.Repositories.LoanApplications;

public class SqliteDbLoanApplicationsRepository : SqliteDbRepository<LoanApplication>, ILoanApplicationsRepository
{
    public SqliteDbLoanApplicationsRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }

    public override async Task UpdateObjectAsync(LoanApplication item)
    {
#pragma warning disable 8603
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            var old = await iMuseumDbContext.Set<LoanApplication>().FirstOrDefaultAsync(old => item.Id == old.Id);
            //Check if actually exists an item with that id
            if (old == null)
            {
                //If not add it
                await this.AddAsync(item);
                return;
            }
            //Code to change each field
            old.ApplicationDate = item.ApplicationDate;
            old.Artwork = item.Artwork;
            old.Duration = item.Duration;
            old.RelatedMuseum = item.RelatedMuseum;
            old.CurrentStatus = item.CurrentStatus;

            //Save changes
            await iMuseumDbContext.SaveChangesAsync();
        }
#pragma warning restore 8603
    }
}