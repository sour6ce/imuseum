using IMuseum.Persistence.Models;

namespace IMuseum.Persistence.Repositories.Users;

public interface IUsersRepository : IRepository<User>
{
    Task UpdateUserAsync(User item);
}