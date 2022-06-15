using IMuseum.Persistence.Models;
using IMuseum.Persistence;
using IMuseum.Business.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace IMuseum.Business;

public static class Extensions
{
    public static void AddPersistence(this IServiceCollection sc, ConfigurationManager cm)
    {
        // NOTE: Any Dependency related to the persistence layer goes here
        sc.AddDatabaseConnector(cm);
        sc.AddRepositories();
    }
    public static void AddBusinessControllers(this IServiceCollection sc)
    {
        // NOTE: Any Dependency related to controllers goes here
        sc.AddControllers();
        sc.AddControllersWithViews();
    }
    public static ArtworkDto AsDto(this Artwork artwork)
    {
        return new ArtworkDto
        {
            ArtworkId = artwork.ArtworkId,
            Title = artwork.Title,
            Author = artwork.Author,
            CreationDate = artwork.CreationDate,
            AddDate = artwork.AddDate,
            Period = artwork.Period,
            Assessment = artwork.Assessment
        };
    }
}