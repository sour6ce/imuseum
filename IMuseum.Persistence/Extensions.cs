using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Artworks;
using IMuseum.Persistence.Repositories.Images;
using IMuseum.Persistence.Repositories.Loans;
using IMuseum.Persistence.Repositories.LoanApplications;
using IMuseum.Persistence.Repositories.Museums;
using IMuseum.Persistence.Repositories.Paintings;
using IMuseum.Persistence.Repositories.Restorations;
using IMuseum.Persistence.Repositories.Sculptures;
using IMuseum.Persistence.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace IMuseum.Persistence;

public static class Extensions
{
    public static void AddDatabaseConnector(this IServiceCollection sc, ConfigurationManager cm)
    {
        // NOTE: Any dependency inyection related to Database goes here
        sc.AddDbContext<IMuseumContext>(options => options.UseSqlite(cm.GetConnectionString("IMuseumDB")));
    }
    public static void AddRepositories(this IServiceCollection sc)
    {
        // NOTE: Any dependency inyection related to repositories goes here
        sc.AddSingleton<IArtworksRepository, SqliteDbArtworksRepository>();
        sc.AddSingleton<IImagesRepository, SqliteDbImagesRepository>();
        sc.AddSingleton<ILoansRepository, SqliteDbLoansRepository>();
        sc.AddSingleton<ILoanApplicationsRepository, SqliteDbLoanApplicationsRepository>();
        sc.AddSingleton<IMuseumsRepository, SqliteDbMuseumsRepository>();
        sc.AddSingleton<IPaintingsRepository, SqliteDbPaintingsRepository>();
        sc.AddSingleton<IRestorationsRepository, SqliteDbRestorationsRepository>();
        sc.AddSingleton<ISculpturesRepository, SqliteDbSculpturesRepository>();
        sc.AddSingleton<IUsersRepository, SqliteDbUsersRepository>();
    }
}