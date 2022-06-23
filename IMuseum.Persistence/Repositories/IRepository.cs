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
    bool Contains(Guid id);
    Task<bool> ContainsAsync(T item);
    Task<bool> ContainsAsync(Guid id);
    bool Remove(T item);
    bool Remove(Guid id);
    Task<bool> RemoveAsync(T item);
    Task<bool> RemoveAsync(Guid id);
    IEnumerable<T> GetObjects();
    Task<IEnumerable<T>> GetObjectsAsync();
    T? GetObject(Guid id);
    Task<T?> GetObjectAsync(Guid id);
    Task UpdateObjectAsync(T item);
    Task<C> ExecuteOnDbAsync<C>(Func<DbSet<T>, IMuseumContext, Task<C>> asyncFunc);
    Task<C> ExecuteOnDbAsync<C>(Func<DbSet<T>, Task<C>> asyncFunc);
    C ExecuteOnDb<C>(Func<DbSet<T>, IMuseumContext, C> func);
    C ExecuteOnDb<C>(Func<DbSet<T>, C> func);
}