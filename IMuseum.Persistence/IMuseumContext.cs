using Microsoft.EntityFrameworkCore;
using IMuseum.Persistence.Models;

namespace IMuseum.Persistence;

using IMuseum.Persistence.DataSeeding;

public class IMuseumContext : DbContext
{
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
        modelBuilder.Entity<User>().Navigation(x => x.Role).AutoInclude();
        modelBuilder.Entity<Restoration>().Navigation(x => x.Artwork).AutoInclude();
        modelBuilder.Entity<Loan>().Navigation(x => x.Application).AutoInclude();
        modelBuilder.Entity<LoanApplication>().Navigation(x => x.Artwork).AutoInclude();
        modelBuilder.Entity<LoanApplication>().Navigation(x => x.LoanRelated).AutoInclude();

        modelBuilder.Entity<Painting>().ToTable("Paintings");
        modelBuilder.Entity<Sculpture>().ToTable("Sculptures");

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

        //User
        modelBuilder.Entity<User>()
            .HasOne<Role>(x => x.Role)
            .WithMany(x => x.RelatedUsers);

        //Seeding Reference
        modelBuilder.DataSeeding();

        //Global Query Filters for Soft-Delete
        modelBuilder.Entity<DatabaseModel>().HasQueryFilter(x => !x.Deleted);
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}