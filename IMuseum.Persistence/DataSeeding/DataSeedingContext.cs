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
            new Role { Id = 1, Name = "Director" },
            new Role { Id = 2, Name = "Restaurator Sheef" },
            new Role { Id = 3, Name = "Catalog Manager" },
            new Role { Id = 4, Name = "Administrator" },
            new Role { Id = 5, Name = "Visiter" });

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
            CurrentStatus = LoanApplication.Status.OnWait,
            ArtworkId = 1,
            MuseumId = 1,
            LoanId = 1 },
            new LoanApplication { 
            Id = 2, 
            ApplicationDate = new DateTime(2022,1,1),
            Duration = 10,
            CurrentStatus = LoanApplication.Status.OnWait,
            ArtworkId = 1,
            MuseumId = 1,
            LoanId = 2 }
            );
      

      for(int i=10;i<20;i++)
        {
            modelBuilder.Entity<Painting>()
            .HasData(
            new Painting{
                Id = i, 
                Media = "address",
                Style = "Renacence",
                Title = "Monalisa"+i.ToString(),
                Author = "Leonardo da Vinci",
                CreationDate = new DateTime(1503,1,1),
                IncorporatedDate = new DateTime(2002,3,4),
                Period = "Renacence",
                Assessment = (i%10)+1,
                CurrentSatus = Artwork.Status.OnDisplay,
                MuseumId =  i%7 + 1,
                Description = "Estalin Disima",
                RoomId = (i%3)+1}
            );
        }

      for(int i = 1; i<=7; i++)
      {
            modelBuilder.Entity<Artwork>()
                  .HasData(
                  new Artwork { 
                  Id = i, 
                  Author = "Unknown",
                  Title = "The tapice " + i.ToString(),
                  CreationDate = new DateTime(2022,6,23),
                  IncorporatedDate = new DateTime(2022,6,23),
                  Period = "ooold",
                  Assessment = 10,
                  CurrentSatus = (i>2 || i<6) ? Artwork.Status.InRestoration : Artwork.Status.OnDisplay,
                  MuseumId = 1,
                  Description = "It is just biuriful.",
                  RoomId= i%3 +1 }
            );
      }
        
        for(int i=20;i<30;i++)
        {
            modelBuilder.Entity<Sculpture>()
            .HasData(
            new Sculpture{
                Id = i, 
                Material = "Gold",
                Style = "Style",
                Title = "Monalisa"+i.ToString(),
                Author = "Leonardo da Vinci",
                CreationDate = new DateTime(1503,1,1),
                IncorporatedDate = new DateTime(2002,3,4),
                Period = "Renacence",
                Assessment = (i%10)+1,
                CurrentSatus = Artwork.Status.OnDisplay,
                MuseumId = 1,
                Description = "Estalin Disima",
                RoomId = (i%3)+1}
            );
        }

      modelBuilder.Entity<Room>()
            .HasData(
            new Room { Id = 1, Name = "Davinci" },
            new Room { Id = 2, Name = "Gallery" },
            new Room { Id = 3, Name = "Galileo" });
      
      modelBuilder.Entity<Restoration>()
            .HasData(
            new Restoration { 
                  Id = 1,
                  Type = "Type1",
                  StartDate = new DateTime(2012,1,7),
                  EndDate = new DateTime(2012,5,8),
                  ArtworkId = 3},
            new Restoration { 
                  Id = 2,
                  Type = "Type1",
                  StartDate = new DateTime(2014,1,7),
                  EndDate = new DateTime(2015,5,8),
                  ArtworkId = 4},
            new Restoration { 
                  Id = 3, 
                  Type = "Type1",
                  StartDate = new DateTime(2011,1,7),
                  EndDate = new DateTime(2011,10,8),
                  ArtworkId = 5  
                  });

      modelBuilder.Entity<Image>()
            .HasData(
            new Image { 
                  Id = 1,
                  Title = "Title",
                  Bytes = new byte[1],
                  FileExtension = "",
                  Size = 3,
                  ArtworkId = 3 },
            new Image { 
                  Id = 2,
                  Title = "Title",
                  Bytes = new byte[1],
                  FileExtension = "",
                  Size = 3,
                  ArtworkId = 3},
            new Image { 
                  Id = 3, 
                  Title = "Title",
                  Bytes = new byte[1],
                  FileExtension = "",
                  Size = 3,
                  ArtworkId = 3 
                  },
            new Image { 
                  Id = 4, 
                  Title = "Title",
                  Bytes = new byte[1],
                  FileExtension = "",
                  Size = 3,
                  ArtworkId = 3 
                  },
            new Image { 
                  Id = 5, 
                  Title = "Title",
                  Bytes = new byte[1],
                  FileExtension = "",
                  Size = 3,
                  ArtworkId = 3 
                  },
            new Image { 
                  Id = 6, 
                  Title = "Title",
                  Bytes = new byte[1],
                  FileExtension = "",
                  Size = 3,
                  ArtworkId = 3 
                  },
            new Image { 
                  Id = 7, 
                  Title = "Title",
                  Bytes = new byte[1],
                  FileExtension = "",
                  Size = 3,
                  ArtworkId = 3 
                  });
      
      modelBuilder.Entity<User>()
            .HasData(
            new User { 
                  Id = 1,
                  Username = "Foreman Administrator",
                  Password = "admin.psw123//",
                  Email = "example@gmail.com"},
            new User { 
                  Id = 2,
                  Username = "Lorena Manager",
                  Password = "manager.psw123//",
                  Email = "example@gmail.com"},
            new User { 
                  Id = 3, 
                  Username = "Juan Restaurator",
                  Password = "restaurator.psw123//",
                  Email = "example@gmail.com"},
            new User { 
                  Id = 4, 
                  Username = "Harvey Director",
                  Password = "director.psw123//",
                  Email = "example@gmail.com"},
            new User { 
                  Id = 5, 
                  Username = "Pablo Restaurator",
                  Password = "restaurator.psw123//",
                  Email = "example@gmail.com"},
            new User { 
                  Id = 6, 
                  Username = "Dorian Manager",
                  Password = "manager.psw123//",
                  Email = "example@gmail.com"});



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
