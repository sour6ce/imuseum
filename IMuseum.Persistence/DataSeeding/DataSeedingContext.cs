using Microsoft.EntityFrameworkCore;
using IMuseum.Persistence.Models;

namespace IMuseum.Persistence.DataSeeding;

// command: dotnet ef migrations add <migration name> --project IMuseum.Persistence --startup-project IMuseum.Presentation
public static class Seeding 
{
    internal static void DataSeeding(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Museum>()
              .HasData(
               new Museum { Id = Guid.NewGuid(), Name = "Louvre Museum" },
               new Museum { Id = Guid.NewGuid(), Name = "British Museum" },
               new Museum { Id = Guid.NewGuid(), Name = "London Arqueology Museum" },
               new Museum { Id = Guid.NewGuid(), Name = "New York Museum" },
               new Museum { Id = Guid.NewGuid(), Name = "Vatican City Museum" },
               new Museum { Id = Guid.NewGuid(), Name = "Kiko's Gallery" },
               new Museum { Id = Guid.NewGuid(), Name = "LeTize Museum" });

        modelBuilder.Entity<Role>()
              .HasData(
               new Role { Id = Guid.NewGuid(), Name = "Role 1" },
               new Role { Id = Guid.NewGuid(), Name = "Role 2" },
               new Role { Id = Guid.NewGuid(), Name = "Role 3" },
               new Role { Id = Guid.NewGuid(), Name = "Role 4" },
               new Role { Id = Guid.NewGuid(), Name = "Role 5" },
               new Role { Id = Guid.NewGuid(), Name = "Role 6" },
               new Role { Id = Guid.NewGuid(), Name = "Role 7" });

        modelBuilder.Entity<Painting>()
                .HasData(
                new Painting { Id = Guid.NewGuid(), Media = "address" },
                new Painting { Id = Guid.NewGuid(), Media = "address" },
                new Painting { Id = Guid.NewGuid(), Media = "address" },
                new Painting { Id = Guid.NewGuid(), Media = "address" },
                new Painting { Id = Guid.NewGuid(), Media = "address" },
                new Painting { Id = Guid.NewGuid(), Media = "address" },
                new Painting { Id = Guid.NewGuid(), Media = "address" });
        }
    }
}