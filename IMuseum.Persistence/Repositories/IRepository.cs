using IMuseum.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace IMuseum.Persistence.Repositories;

public interface IRepository<T> where T : DatabaseModel
{
    //Like ICollection<T> but also Async
    int Count { get; }
    Task<int> GetCountAsync();
    void Add(T item);
    Task AddAsync(T item);
    bool Contains(T item);
    bool Contains(int id);
    Task<bool> ContainsAsync(T item);
    Task<bool> ContainsAsync(int id);
    bool Remove(T item);
    bool Remove(int id);
    Task<bool> RemoveAsync(T item);
    Task<bool> RemoveAsync(int id);
    IEnumerable<T> GetObjects();
    Task<IEnumerable<T>> GetObjectsAsync();
    T? GetObject(int id);
    Task<T?> GetObjectAsync(int id);
    Task UpdateObjectAsync(T item);
    Task<C> ExecuteOnDbAsync<C>(Func<DbSet<T>, IMuseumContext, Task<C>> asyncFunc);
    Task<C> ExecuteOnDbAsync<C>(Func<DbSet<T>, Task<C>> asyncFunc);
    C ExecuteOnDb<C>(Func<DbSet<T>, IMuseumContext, C> func);
    C ExecuteOnDb<C>(Func<DbSet<T>, C> func);
}