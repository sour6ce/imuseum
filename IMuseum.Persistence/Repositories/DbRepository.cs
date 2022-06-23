using Microsoft.EntityFrameworkCore;
using IMuseum.Persistence.Models;
using Microsoft.Extensions.DependencyInjection;

namespace IMuseum.Persistence.Repositories;

public abstract class DbRepository<T> : IRepository<T> where T : DatabaseModel
{
    protected readonly IServiceProvider serviceProvider;

    public virtual async Task<int> GetCountAsync(CancellationToken cancellationToken)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            return await iMuseumDbContext.Set<T>().CountAsync(cancellationToken);
        }
    }

    public virtual int Count
    {
        get
        {
            {
                using (var scope = this.serviceProvider.CreateScope())
                {
                    var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
                    return iMuseumDbContext.Set<T>().Count();
                }

            }
        }
    }

    public virtual bool IsReadOnly => false;

    public DbRepository(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public virtual async Task AddAsync(T item)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            DbSet<T> tempset = iMuseumDbContext.Set<T>();
            var chck = await tempset.ContainsAsync(item);
            if (!chck)
                await tempset.AddAsync(item);
            await iMuseumDbContext.SaveChangesAsync();
        }
    }

    public virtual async Task<bool> ContainsAsync(T item)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            DbSet<T> tempset = iMuseumDbContext.Set<T>();
            return await tempset.ContainsAsync(item);
        }
    }

    public virtual async Task<bool> RemoveAsync(T item)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            DbSet<T> tempset = iMuseumDbContext.Set<T>();
            var chck = await tempset.ContainsAsync(item);
            if (!chck)
                await tempset.Where(x => x.Id == item.Id).ForEachAsync((x) =>
                {
                    x.Deleted = true;
                    x.DeletedTime = DateTime.Now;
                });
            await iMuseumDbContext.SaveChangesAsync();
            return chck;
        }
    }

    public virtual async Task<int> GetCountAsync() => await GetCountAsync();

    public virtual void Add(T item)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            DbSet<T> tempset = iMuseumDbContext.Set<T>();
            var chck = tempset.ContainsAsync(item);
            if (!chck.Result)
                iMuseumDbContext.Set<T>().Add(item);
            iMuseumDbContext.SaveChanges();
        }
    }

    public virtual bool Contains(T item)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            DbSet<T> tempset = iMuseumDbContext.Set<T>();
            return tempset.Contains(item);
        }
    }

    public virtual bool Remove(T item)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            DbSet<T> tempset = iMuseumDbContext.Set<T>();
            var chck = tempset.ContainsAsync(item);
            if (!chck.Result)
                tempset.Where(x => x.Id == item.Id).ForEachAsync((x) =>
                  {
                      x.Deleted = true;
                      x.DeletedTime = DateTime.Now;
                  }).Wait();
            iMuseumDbContext.SaveChanges();
            return chck.Result;
        }
    }

    public virtual IEnumerable<T> GetObjects()
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            DbSet<T> tempset = iMuseumDbContext.Set<T>();
            foreach (var item in tempset)
            {
                yield return item;
            }
        }
    }

    public virtual async Task<IEnumerable<T>> GetObjectsAsync()
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            return await iMuseumDbContext.Set<T>().ToListAsync<T>();
        }
    }

    public virtual async Task<T?> GetObjectAsync(int id)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var tempset = scope.ServiceProvider.GetRequiredService<IMuseumContext>().Set<T>();
            return await tempset.FirstOrDefaultAsync();
        }
    }

    public bool Contains(int id)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            DbSet<T> tempset = iMuseumDbContext.Set<T>();
            return tempset.Find(id) != null;
        }
    }

    public async Task<bool> ContainsAsync(int id)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            DbSet<T> tempset = iMuseumDbContext.Set<T>();
            return (await tempset.FindAsync(id)) != null;
        }
    }

    public bool Remove(int id)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            DbSet<T> tempset = iMuseumDbContext.Set<T>();
            var chck = this.ContainsAsync(id);
            if (!chck.Result)
                tempset.Where(x => x.Id == id).ForEachAsync((x) =>
                  {
                      x.Deleted = true;
                      x.DeletedTime = DateTime.Now;
                  }).Wait();
            iMuseumDbContext.SaveChanges();
            return chck.Result;
        }
    }

    public async Task<bool> RemoveAsync(int id)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            DbSet<T> tempset = iMuseumDbContext.Set<T>();
            var chck = this.ContainsAsync(id);
            if (await chck)
                await tempset.Where(x => x.Id == id).ForEachAsync((x) =>
                  {
                      x.Deleted = true;
                      x.DeletedTime = DateTime.Now;
                  });
            await iMuseumDbContext.SaveChangesAsync();
            return await chck;
        }
    }

    public T? GetObject(int id)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var tempset = scope.ServiceProvider.GetRequiredService<IMuseumContext>().Set<T>();
            return tempset.FirstOrDefault();
        }
    }

    public abstract Task UpdateObjectAsync(T item);

    public async Task<C> ExecuteOnDbAsync<C>(Func<DbSet<T>, IMuseumContext, Task<C>> asyncFunc)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            DbSet<T> tempset = iMuseumDbContext.Set<T>();
            var result = await asyncFunc(tempset, iMuseumDbContext);
            return result;
        }
    }

    public async Task<C> ExecuteOnDbAsync<C>(Func<IQueryable<T>, Task<C>> asyncFunc)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            DbSet<T> tempset = iMuseumDbContext.Set<T>();
            var result = await asyncFunc(tempset);
            return result;
        }
    }

    public C ExecuteOnDb<C>(Func<DbSet<T>, IMuseumContext, C> func)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            DbSet<T> tempset = iMuseumDbContext.Set<T>();
            var result = func(tempset, iMuseumDbContext);
            return result;
        }
    }

    public C ExecuteOnDb<C>(Func<IQueryable<T>, C> func)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<IMuseumContext>();
            DbSet<T> tempset = iMuseumDbContext.Set<T>();
            var result = func(tempset);
            return result;
        }
    }
}