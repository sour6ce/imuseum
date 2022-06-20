using Microsoft.EntityFrameworkCore;

namespace IMuseum.Persistence.Models;

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
            .HasOne<Museum>(x => x.Museum);

        //Room => Artwork is a one to many relationship
        modelBuilder.Entity<Room>()
            .HasMany<Artwork>(r => r.Artworks);

        //Restoration => Artwork is a one to one relationship
        modelBuilder.Entity<Restoration>()
            .HasOne<Artwork>(r => r.Artwork);

        //Loan => LoanApplication is a many to one relationship
        modelBuilder.Entity<Loan>()
            .HasOne<LoanApplication>(l => l.Application);

        //LoanApplication => Museum is a many to one relationship
        modelBuilder.Entity<LoanApplication>()
            .HasOne<Museum>(la => la.RelatedMuseum);

        //LoanApplication => Artwork is a many to one relationship
        modelBuilder.Entity<LoanApplication>()
            .HasOne<Artwork>(la => la.Artwork);

        //Artwork => Image is a one to many relationship
        modelBuilder.Entity<Artwork>()
            .HasMany<Image>(i => i.Images);

        //User
        modelBuilder.Entity<User>()
            .HasMany<Role>(x => x.Roles);
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}