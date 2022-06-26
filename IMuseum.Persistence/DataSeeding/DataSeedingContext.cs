using Microsoft.EntityFrameworkCore;
using IMuseum.Persistence.Models;

namespace IMuseum.Persistence.DataSeeding;

// command to update db: dotnet ef database update --project IMuseum.Persistence --startup-project IMuseum.Presentation
// command migration: dotnet ef migrations add <migration name> --project IMuseum.Persistence --startup-project IMuseum.Presentation
public static class Seeding 
{
    internal static void DataSeeding(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Museum>()
              .HasData(
               new Museum { Id = 1, Name = "Louvre Museum" },
               new Museum { Id = 2, Name = "British Museum" },
               new Museum { Id = 3, Name = "London Arqueology Museum" },
               new Museum { Id = 4, Name = "New York Museum" },
               new Museum { Id = 5, Name = "Vatican City Museum" },
               new Museum { Id = 6, Name = "Kiko's Gallery" },
               new Museum { Id = 7, Name = "LeTize Museum" });

        modelBuilder.Entity<Role>()
              .HasData(
               new Role { Id = 8, Name = "Role 1" },
               new Role { Id = 9, Name = "Role 2" },
               new Role { Id = 10, Name = "Role 3" },
               new Role { Id = 11, Name = "Role 4" },
               new Role { Id = 12, Name = "Role 5" },
               new Role { Id = 13, Name = "Role 6" },
               new Role { Id = 14, Name = "Role 7" });
        
        modelBuilder.Entity<Loan>()
              .HasData(
                new Loan { 
                Id = 1, 
                StartDate = new DateTime(2022,6,23),
                PaymentAmount = 10,
                LoanAplicationId = 1 },
                new Loan { 
                Id = 2, 
                StartDate = new DateTime(2022,3,29),
                PaymentAmount = 20,
                LoanAplicationId = 2 }
                );

        modelBuilder.Entity<LoanApplication>()
              .HasData(
                new LoanApplication { 
                Id = 1, 
                ApplicationDate = new DateTime(2022,6,23),
                Duration = 10,
                CurrentStatus = LoanApplication.LoanApplicationStatus.OnWait,
                ArtworkId = 1,
                MuseumId = 1,
                LoanId = 1 },
                new LoanApplication { 
                Id = 2, 
                ApplicationDate = new DateTime(2022,1,1),
                Duration = 10,
                CurrentStatus = LoanApplication.LoanApplicationStatus.OnWait,
                ArtworkId = 1,
                MuseumId = 1,
                LoanId = 2 }
                );

        
        modelBuilder.Entity<Artwork>()
              .HasData(
                new Artwork { 
                Id = 1, 
                Author = "Unknown",
                Title = "The tapice",
                CreationDate = new DateTime(2022,6,23),
                IncorporatedDate = new DateTime(2022,6,23),
                Period = "ooold",
                Assessment = 10,
                CurrentSatus=Artwork.ArtworkStatus.OnDisplay, 
                MuseumId = 1,
                Description = "It is just a simple tapice.",
                RoomId=1}
                );

        modelBuilder.Entity<Room>()
              .HasData(
               new Room { Id = 1, Name = "Davinci" },
               new Room { Id = 2, Name = "Gallery" },
               new Room { Id = 3, Name = "Galileo" });

        // modelBuilder.Entity<Painting>()
        //         .HasData(
        //         new Painting { 
        //             Id = 15, 
        //             Media = "address",
        //             Title = "Monalisa",
        //             Author = "Leonardo da Vinci",
        //             CreationDate = new DateTime(1503,1,1),
        //             IncorporatedDate = new DateTime(2002,3,4),
        //             Period = "Renacence",
        //             Assessment = 10,
        //             CurrentSatus = Artwork.Status.OnDisplay,
        //             MuseumId =  1,
        //             Museum = null,
        //             RoomId =  22,
        //             //Room = ,
        //             //Restorations=,
        //             //LoanApplications=,                  
        //             },
        //         new Painting { 
        //             Id = 16, 
        //             Media = "address",
        //             Title = "School of Athens",
        //             Author = "Raphael",
        //             CreationDate = new DateTime(1500,1,1),
        //             IncorporatedDate = new DateTime(2005,3,4),
        //             Period = "Renacence",
        //             Assessment = 10,
        //             CurrentSatus = Artwork.Status.OnDisplay,
        //             MuseumId = 1,
        //             Museum = null,
        //             RoomId = 22,
        //             //Room = ,
        //             //Restorations=,
        //             //LoanApplications=, 
        //             },
        //         new Painting { 
        //             Id = 17, 
        //             Media = "address",                    
        //             Title = "Night Watch",
        //             Author = "Rembrandt",
        //             CreationDate = new DateTime(1600,1,1),
        //             IncorporatedDate = new DateTime(2004,3,4),
        //             Period = "Renacence",
        //             Assessment = 9,
        //             CurrentSatus = Artwork.Status.InRestoration,
        //             MuseumId = 2,
        //             Museum = null,
        //             RoomId = 22,
        //             //Room = ,
        //             //Restorations=,
        //             //LoanApplications=, 
        //             },
        //         new Painting { 
        //             Id = 18, 
        //             Media = "address",
        //             Title = "Beheading of Saint John the Baptist",
        //             Author = "Caravaggio",
        //             CreationDate = new DateTime(1400,3,8),
        //             IncorporatedDate = new DateTime(2002,3,4),
        //             Period = "Renacence",
        //             Assessment = 10,
        //             CurrentSatus = Artwork.Status.OnDisplay,
        //             MuseumId = 3,
        //             Museum = null,
        //             RoomId = 22,
        //             //Room = ,
        //             //Restorations=,
        //             //LoanApplications=, 
        //              },
        //         new Painting { 
        //             Id = 19, 
        //             Media = "address",
        //             Title = "The Last Supper",
        //             Author = "Leonardo da Vinci",
        //             CreationDate = new DateTime(1400,1,1),
        //             IncorporatedDate = new DateTime(2002,3,4),
        //             Period = "Renacence",
        //             Assessment = 10,
        //             CurrentSatus = Artwork.Status.InStorage,
        //             MuseumId = 1,
        //             Museum = null,
        //             RoomId = 22,
        //             //Room = ,
        //             //Restorations=,
        //             //LoanApplications=, 
        //              },
        //         new Painting { 
        //             Id = 20, 
        //             Media = "address",
        //             Title = "The Starry Night",
        //             Author = "Vincent van Gogh",
        //             CreationDate = new DateTime(1889,1,1),
        //             IncorporatedDate = new DateTime(2009,3,4),
        //             Period = "Renacence",
        //             Assessment = 10,
        //             CurrentSatus = Artwork.Status.OnLoan,
        //             MuseumId = 4,
        //             Museum = null,
        //             RoomId = 22,
        //             //Room = ,
        //             //Restorations=,
        //             //LoanApplications=, 
        //              },
        //         new Painting { 
        //             Id = 21, 
        //             Media = "address",
        //             Title = "The Scream",
        //             Author = "Edvard Munch",
        //             CreationDate = new DateTime(1893,1,1),
        //             IncorporatedDate = new DateTime(2002,3,4),
        //             Period = "Renacence",
        //             Assessment = 10,
        //             CurrentSatus = Artwork.Status.OnDisplay,
        //             MuseumId = 5,
        //             Museum = null,
        //             RoomId = 22,
        //             //Room = ,
        //             //Restorations=,
        //             //LoanApplications=, 
        //              });
        //
        // modelBuilder.Entity<Sculpture>()
        //         .HasData(
        //         new Sculpture { 
        //             Id = 15, 
        //             Material = "Gold",
        //             Style = "Style",
        //             Title = "Monalisa",
        //             Author = "Leonardo da Vinci",
        //             CreationDate = new DateTime(1503,1,1),
        //             IncorporatedDate = new DateTime(2002,3,4),
        //             Period = "Renacence",
        //             Assessment = 10,
        //             CurrentSatus = Artwork.Status.OnDisplay,
        //             MuseumId = 1,
        //             Museum = null,
        //             RoomId = 22,
        //             //Room = ,
        //             //Restorations=,
        //             //LoanApplications=,                  
        //             },
        //         new Sculpture { 
        //             Id = 16, 
        //             Material = "Iron",
        //             Style = "Style",
        //             Title = "School of Athens",
        //             Author = "Raphael",
        //             CreationDate = new DateTime(1500,1,1),
        //             IncorporatedDate = new DateTime(2005,3,4),
        //             Period = "Renacence",
        //             Assessment = 10,
        //             CurrentSatus = Artwork.Status.OnDisplay,
        //             MuseumId = 1,
        //             Museum = null,
        //             RoomId = 22,
        //             //Room = ,
        //             //Restorations=,
        //             //LoanApplications=, 
        //             },
        //         new Sculpture { 
        //             Id = 17, 
        //             Material = "Wood",
        //             Style = "Style",                    
        //             Title = "Night Watch",
        //             Author = "Rembrandt",
        //             CreationDate = new DateTime(1600,1,1),
        //             IncorporatedDate = new DateTime(2004,3,4),
        //             Period = "Renacence",
        //             Assessment = 9,
        //             CurrentSatus = Artwork.Status.InRestoration,
        //             MuseumId = 2,
        //             Museum = null,
        //             RoomId = 22,
        //             //Room = ,
        //             //Restorations=,
        //             //LoanApplications=, 
        //             },
        //         new Sculpture { 
        //             Id = 18, 
        //             Material = "Glass",
        //             Style = "Style",
        //             Title = "Beheading of Saint John the Baptist",
        //             Author = "Caravaggio",
        //             CreationDate = new DateTime(1400,3,8),
        //             IncorporatedDate = new DateTime(2002,3,4),
        //             Period = "Renacence",
        //             Assessment = 10,
        //             CurrentSatus = Artwork.Status.OnDisplay,
        //             MuseumId = 3,
        //             Museum = null,
        //             RoomId = 22,
        //             //Room = ,
        //             //Restorations=,
        //             //LoanApplications=, 
        //              },
        //         new Sculpture { 
        //             Id = 19,
        //             Style = "Style",
        //             Material = "Ice",
        //             Title = "The Last Supper",
        //             Author = "Leonardo da Vinci",
        //             CreationDate = new DateTime(1400,1,1),
        //             IncorporatedDate = new DateTime(2002,3,4),
        //             Period = "Renacence",
        //             Assessment = 10,
        //             CurrentSatus = Artwork.Status.InStorage,
        //             MuseumId = 1,
        //             Museum = null,
        //             RoomId = 22,
        //             //Room = ,
        //             //Restorations=,
        //             //LoanApplications=,  
        //              });
        }

}
