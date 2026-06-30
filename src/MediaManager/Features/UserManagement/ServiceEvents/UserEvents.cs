using MediaManager.Features.UserManagement.Abstractions.Models;

namespace MediaManager.Features.UserManagement.ServiceEvents;

public record CreateUserCommand(string Username, bool IsActive, DateTime? LastLogin) : UserRecord(Username, IsActive, LastLogin);
public record UserCreatedResponse(string Username, bool IsActive, DateTime? LastLogin) : UserRecord(Username, IsActive, LastLogin);
public record DeleteUserCommand(string Username) : UserRecord(Username, false, null);
public record UserDeletedResponse(string Username) : UserRecord(Username, false, null);
public record GetUserCommand(string Username) : UserRecord(Username, false, null);
public record GetUserResponse(string Username, bool IsActive, DateTime? LastLogin) : UserRecord(Username, IsActive, LastLogin);
public record UpdateUserCommand(string Username, bool IsActive, DateTime? LastLogin) : UserRecord(Username, IsActive, LastLogin);
public record UserUpdatedResponse(string Username, bool IsActive, DateTime? LastLogin) : UserRecord(Username, IsActive, LastLogin);