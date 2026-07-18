using MediaManager.Shared.Domain.Models;
using MediaManager.Infrastructure.Persistence;
using MediaManager.Features.UserManagement.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.Features.UserManagement.Repositories;

internal class UserRepository(MediaManagerDbContext dbContext) : IUserManagementRepository
{
    public async Task AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUserAsync(User user, CancellationToken cancellationToken = default)
    {
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Username == userName, cancellationToken);
    }

    public async Task<IEnumerable<User>> ListUsersAsync(int? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var query = dbContext.Users.AsQueryable();

        if (pageNumber.HasValue && pageSize.HasValue)
        {
            query = query.Skip(pageNumber.Value * pageSize.Value).Take(pageSize.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task UpdateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}