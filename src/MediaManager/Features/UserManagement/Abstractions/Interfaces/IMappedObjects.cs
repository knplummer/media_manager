namespace MediaManager.Features.UserManagement.Abstractions.Interfaces;

public interface IUser
{
    string Username { get; init; }
    bool IsActive { get; init; }
    DateTime? LastLogin { get; init; }
}
