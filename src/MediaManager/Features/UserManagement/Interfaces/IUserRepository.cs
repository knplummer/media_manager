using MediaManager.Shared.Domain;

namespace MediaManager.Features.UserManagement.Interfaces;

public interface IUserRepository
{
    Task<User?> GetuserByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> ListUsersAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task AddUserAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(User user, CancellationToken cancellationToken = default);
}