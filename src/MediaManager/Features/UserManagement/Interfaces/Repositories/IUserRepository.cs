using MediaManager.Shared.Domain;

namespace MediaManager.Features.UserManagement.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> ListUsersAsync(int? pageNumber = null, int? pageSize = null, CancellationToken cancellationToken = default);
    Task AddUserAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(User user, CancellationToken cancellationToken = default);
}