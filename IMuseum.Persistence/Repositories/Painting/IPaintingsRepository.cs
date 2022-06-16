using IMuseum.Persistence.Models;

namespace IMuseum.Persistence.Repositories.Paintings;

public interface IPaintingsRepository : IRepository<Painting>
{
    Task UpdatePaintingAsync(Painting item);
}