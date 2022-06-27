using  IMuseum.Persistence.Models;
using IMuseum.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IMuseum.Auth.Services;

public interface IUserService
{
    Task<User> Authenticate(string username, string password);
    Task<IEnumerable<User>> GetAll();
}

public class UserService : IUserService
{
    public async Task<User> Authenticate(string username, string password)
    {
        DbContextOptions<IMuseumContext> dbContextOptions = new DbContextOptions<IMuseumContext>();
        using(var context = new IMuseumContext(dbContextOptions))
        {
            // wrapped in "await Task.Run" to mimic fetching user from a db
            var user = await Task.Run(() => context.Set<User>().SingleOrDefault(x => x.Username == username && x.Password == password));

            // on auth fail: null is returned because user is not found
            // on auth success: user object is returned
            return user;
        }
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        DbContextOptions<IMuseumContext> dbContextOptions = new DbContextOptions<IMuseumContext>();
        using(var context = new IMuseumContext(dbContextOptions))
        {
            // wrapped in "await Task.Run" to mimic fetching user from a db
            var users = await Task.Run(() => context.Set<User>());

            // on auth fail: null is returned because user is not found
            // on auth success: user object is returned
            return users;
        }
    }
}
