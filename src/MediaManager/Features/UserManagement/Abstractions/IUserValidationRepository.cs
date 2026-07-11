using MediaManager.Shared.Domain.Models;

namespace MediaManager.Features.UserManagement.Abstractions.Interfaces;

public interface IUserValidationRepository
{
    Task<bool> UserExistsAsync(string username);
}