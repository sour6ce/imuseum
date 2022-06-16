using IMuseum.Persistence.Models;

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
}