using IMuseum.Persistence.Models;

namespace IMuseum.Persistence.Repositories.Roles;

public interface IRolesRepository : IRepository<Role>
{
    Task UpdateRoleAsync(Role item);
}