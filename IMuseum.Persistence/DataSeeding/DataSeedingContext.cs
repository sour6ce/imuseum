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
                new Loan
                {
                    Id = 1,
                    StartDate = new DateTime(2022, 6, 23),
                    PaymentAmount = 10,
                    LoanApplicationId = 1
                },
                new Loan
                {
                    Id = 2,
                    StartDate = new DateTime(2022, 3, 29),
                    PaymentAmount = 20,
                    LoanApplicationId = 2
                }
            );

        modelBuilder.Entity<LoanApplication>()
            .HasData(
            new LoanApplication
            {
                Id = 1,
                ApplicationDate = new DateTime(2022, 6, 23),
                Duration = 10,
                CurrentStatus = LoanApplication.LoanApplicationStatus.OnWait,
                ArtworkId = 1,
                MuseumId = 1
            },
                new LoanApplication
                {
                    Id = 2,
                    ApplicationDate = new DateTime(2022, 1, 1),
                    Duration = 10,
                    CurrentStatus = LoanApplication.LoanApplicationStatus.OnWait,
                    ArtworkId = 1,
                    MuseumId = 1
                }
            );

        string[] paintingsNames = {"The Great Bathers", "Vincent’s Bedroom in Arles",
                        "The Sea of Ice","Saturn Devouring His Son",
                        "Les Demoiselles d’Avignon","Dance at Moulin de la Galette",
                        "Time transfixed","Olympia, Musee d’Orsay, Paris",
                        "The Son of Man","Arrangement in Grey and Black. Portrait of the Painter’s Mother",
                        "American Gothic","A Sunday Afternoon on the Island of La Grande Jatte",
                        "Water Lilies","The Kiss (Bacio)",
                        "Night Watch","The Girl with a Pearl Earring",
                        "Three Musicians","The Scream","Starry-Night","Mona Lisa (La Gioconda)"};

        string[] paintingImages = {"https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:265/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Leonardo-Da-Vinci-Mona-Lisa.jpg",
"https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:500/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Vincent-Van-Gogh-Starry-Night.jpg",
"https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:316/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Edvard-Munch-The-Scream.jpg",
"https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:443/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/PabloPicasso-ThreeMusicians.jpg",
"https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:347/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Jan-Vermeer-The-Girl-with-a-Pearl-Earring.jpg",
"https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:492/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Rembrandt_Van_Rijn-Night_Watch_.jpg",
"https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:398/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Gustav-Klimt-The-Kiss-Bacio-.jpg",
"https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:413/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Claude-Monet-Water-Lilies.jpg",
"https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:592/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Georges-Seurat-A-Sunday-Afternoon-on-the-Island-of-La-Grande-Jatte.jpg",
"https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:331/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Grant-Wood-American-Gothic.jpg"};

        for (int i = 19; i >= 10; i--)
        {
            modelBuilder.Entity<Painting>()
                .HasData(
                new Painting
                {
                    Id = i,
                    Media = "address",
                    Image = paintingImages[19 - i],
                    Style = "Renacence",
                    Title = paintingsNames[i],
                    Author = "Author",
                    CreationDate = new DateTime(1503, 1, 1),
                    IncorporatedDate = new DateTime(2002, 3, 4),
                    Period = "Renacence",
                    Assessment = (i % 10) + 1,
                    CurrentSatus = Artwork.ArtworkStatus.OnDisplay,
                    MuseumId = i % 7 + 1,
                    Description = "Estalin Disima",
                    RoomId = (i % 3) + 1
                }
            );
        }

        for (int i = 1; i <= 7; i++)
        {
            modelBuilder.Entity<Artwork>()
                .HasData(
                new Artwork
                {
                    Id = i,
                    Author = "Unknown",
                    Title = "The tapice " + i.ToString(),
                    Image = "",
                    CreationDate = new DateTime(2022, 6, 23),
                    IncorporatedDate = new DateTime(2022, 6, 23),
                    Period = "ooold",
                    Assessment = 10,
                    CurrentSatus = (i > 2 || i < 6) ? Artwork.ArtworkStatus.InRestoration : Artwork.ArtworkStatus.OnDisplay,
                    MuseumId = 1,
                    Description = "It is just biuriful.",
                    RoomId = i % 3 + 1
                }
            );
        }

        for (int i = 20; i < 30; i++)
        {
            modelBuilder.Entity<Sculpture>()
                .HasData(
                new Sculpture
                {
                    Id = i,
                    Material = "Gold",
                    Image = "",
                    Style = "Style",
                    Title = "Monalisa" + i.ToString(),
                    Author = "Leonardo da Vinci",
                    CreationDate = new DateTime(1503, 1, 1),
                    IncorporatedDate = new DateTime(2002, 3, 4),
                    Period = "Renacence",
                    Assessment = (i % 10) + 1,
                    CurrentSatus = Artwork.ArtworkStatus.OnDisplay,
                    MuseumId = 1,
                    Description = "Estalin Disima",
                    RoomId = (i % 3) + 1
                }
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
                Type = Restoration.RestorationType.AestheticFunctional,
                StartDate = new DateTime(2022,6,28),
                EndDate = new DateTime(2023,1,1),
                ArtworkId = 10
            },
            new Restoration {
                Id = 2,
                Type = Restoration.RestorationType.Scientific,
                StartDate = new DateTime(2021,1,28),
                EndDate = new DateTime(2022,1,1),
                ArtworkId = 15
            },
            new Restoration {
                Id = 3,
                Type = Restoration.RestorationType.Commercial,
                StartDate = new DateTime(2020,5,27),
                EndDate = new DateTime(2024,1,1),
                ArtworkId = 2
            }
        );

        modelBuilder.Entity<User>()
            .HasData(
            new User
            {
                Id = 1,
                Username = "Foreman Administrator",
                Password = "admin.psw123//",
                Email = "example@gmail.com",
                RoleId = 4
            },
            new User
            {
                Id = 2,
                Username = "Lorena Manager",
                Password = "manager.psw123//",
                Email = "example@gmail.com",
                RoleId = 3
            },
            new User
            {
                Id = 3,
                Username = "Juan Restaurator",
                Password = "restaurator.psw123//",
                Email = "example@gmail.com",
                RoleId = 2
            },
            new User
            {
                Id = 4,
                Username = "Harvey Director",
                Password = "director.psw123//",
                Email = "example@gmail.com",
                RoleId = 1
            },
            new User
            {
                Id = 5,
                Username = "Pablo Restaurator",
                Password = "restaurator.psw123//",
                Email = "example@gmail.com",
                RoleId = 2
            },
            new User
            {
                Id = 6,
                Username = "Dorian Manager",
                Password = "manager.psw123//",
                Email = "example@gmail.com",
                RoleId = 3
            });

    }

}
