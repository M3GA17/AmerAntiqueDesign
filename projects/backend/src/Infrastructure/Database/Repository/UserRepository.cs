using Domain.UserManagement;
using Domain.UserManagement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository;
public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    public async Task AddAsync(User entity, CancellationToken cancellationToken)
    {
        await dbContext.Users.AddAsync(entity, cancellationToken);
    }
    public async Task<bool> ExistsAsync(IdUser id, CancellationToken cancellationToken)
    {
        return await dbContext.Users.AnyAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<User?> GetAsync(IdUser id, CancellationToken cancellationToken)
    {
        return await dbContext.Users.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
    public async Task<IEnumerable<User>> GetListAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .Include(u => u.UserRoleMappings)
            .ThenInclude(urm => urm.Role)
            .Include(u => u.UserGroupMemberships)
            .ThenInclude(ugm => ugm.Group)
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task RemoveAsync(IdUser id, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (user is not null)
        {
            dbContext.Users.Remove(user);
        }
    }
}