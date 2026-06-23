using MediaManager.Shared.Domain;
using MediaManager.Infrastructure.Persistence;
using MediaManager.Features.UserManagement.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.Features.UserManagement.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MediaManagerDbContext _dbContext;

    public UserRepository(MediaManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUserAsync(User user, CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetuserByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<User>> ListUsersAsync(int? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {   
        var query = _dbContext.Users.AsQueryable();

        if (pageNumber.HasValue && pageSize.HasValue)
        {
            query = query.Skip(pageNumber.Value * pageSize.Value).Take(pageSize.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task UpdateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}