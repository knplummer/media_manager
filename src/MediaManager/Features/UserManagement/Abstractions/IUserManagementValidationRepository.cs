using MediaManager.Shared.Domain.Models;

namespace MediaManager.Features.UserManagement.Abstractions.Interfaces;

public interface IUserManagementValidationRepository
{
    Task<bool> UserExistsAsync(string username);
}