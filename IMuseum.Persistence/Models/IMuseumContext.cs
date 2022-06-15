using Microsoft.EntityFrameworkCore;

namespace IMuseum.Persistence.Models;

public class IMuseumContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Restoration> Restorations { get; set; }
    public DbSet<Painting> Paintings { get; set; }
    public DbSet<Sculpture> Sculptures { get; set; }
    public DbSet<PlasticArt> PlasticArts { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<FriendMuseum> FriendMuseums { get; set; }
    public DbSet<Artwork> Artworks { get; set; }
    public DbSet<ArtworkInPosess> ArtworksInPosess { get; set; }

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
        //Inheritance implemented with Table-per-type configuration
        modelBuilder.Entity<Artwork>().ToTable("Artwork");
        modelBuilder.Entity<ArtworkInPosess>().ToTable("ArtworkInPosess ");
        modelBuilder.Entity<PlasticArt>().ToTable("PlasticArt ");
        modelBuilder.Entity<Painting>().ToTable("Painting ");
        modelBuilder.Entity<Sculpture>().ToTable("Sculpture ");

        //Room => ArtworkInPosess is a one to many relationship
        modelBuilder.Entity<Room>()
            .HasMany(r => r.Artworks);

        //Restoration => ArtworkInPosess is a one to one relationship
        modelBuilder.Entity<Restoration>()
            .HasOne(r => r.Artwork);

        //Loan => LoanApplication is a one to one relationship
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Application);

        //LoanApplication => FriendMuseum is a one to one relationship
        modelBuilder.Entity<LoanApplication>()
            .HasOne(la => la.Museum);

        //LoanApplication => ArtworkInPosess is a one to one relationship
        modelBuilder.Entity<LoanApplication>()
            .HasOne(la => la.Artwork);
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}