using MediaManager.Shared.Domain.Models;

namespace MediaManager.Features.UserManagement.Abstractions.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task AddUserAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(User user, CancellationToken cancellationToken = default);
}