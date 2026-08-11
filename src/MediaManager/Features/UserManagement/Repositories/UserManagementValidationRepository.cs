using MediaManager.Infrastructure.Persistence;
using MediaManager.Features.UserManagement.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.Features.UserManagement.Repositories;

public class UserManagementValidationRepository(MediaManagerDbContext dbContext) : IUserManagementValidationRepository
{
    public async Task<bool> UserExistsAsync(string username)
    {
        return await dbContext.Users.AnyAsync(u => u.Username == username);
    }
}