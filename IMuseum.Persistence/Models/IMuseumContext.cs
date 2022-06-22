using Microsoft.EntityFrameworkCore;
namespace IMuseum.Persistence.Models;

using IMuseum.Persistence.DataSeeding;
public class IMuseumContext : DbContext
{
    // public DbSet<DatabaseModel> DbModels { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Restoration> Restorations { get; set; }
    public DbSet<Painting> Paintings { get; set; }
    public DbSet<Sculpture> Sculptures { get; set; }
    public DbSet<PlasticArt> PlasticArts { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Museum> Museums { get; set; }
    public DbSet<Artwork> Artworks { get; set; }
    public DbSet<Image> Images { get; set; }

    public string DbPath { get; }

    public IMuseumContext(DbContextOptions<IMuseumContext> options) : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join("..", Path.Join("IMuseum.Db", "IMuseum.db"));
    }

    //The following configures the model's inheritances and foreign relationships
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Artwork => Museum is a many to one relationship
        modelBuilder.Entity<Artwork>()
            .HasOne<Museum>(x => x.Museum)
            .WithMany(x => x.Artworks);

        //Room => Artwork is a one to many relationship
        modelBuilder.Entity<Room>()
            .HasMany<Artwork>(r => r.Artworks)
            .WithOne(x => x.Room);

        //Restoration => Artwork is a one to one relationship
        modelBuilder.Entity<Restoration>()
            .HasOne<Artwork>(r => r.Artwork)
            .WithMany(x => x.Restorations);

        //Loan => LoanApplication is a many to one relationship
        modelBuilder.Entity<Loan>()
            .HasOne<LoanApplication>(l => l.Application)
            .WithOne(x => x.LoanRelated);

        //LoanApplication => Museum is a many to one relationship
        modelBuilder.Entity<LoanApplication>()
            .HasOne<Museum>(la => la.RelatedMuseum)
            .WithMany(x => x.LoanApplications);

        //LoanApplication => Artwork is a many to one relationship
        modelBuilder.Entity<LoanApplication>()
            .HasOne<Artwork>(la => la.Artwork)
            .WithMany(x => x.LoanApplications);

        //Artwork => Image is a one to many relationship
        modelBuilder.Entity<Artwork>()
            .HasMany<Image>(i => i.Images)
            .WithOne(x => x.Artwork);

        //User
        modelBuilder.Entity<User>()
            .HasMany<Role>(x => x.Roles)
            .WithMany(x => x.RelatedUsers);
        
        modelBuilder.DataSeeding();
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}