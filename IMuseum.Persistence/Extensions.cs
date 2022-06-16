using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Artworks;
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
    }
}