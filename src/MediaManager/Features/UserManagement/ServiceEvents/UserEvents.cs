using MediaManager.Features.UserManagement.Abstractions.Interfaces;

namespace MediaManager.Features.UserManagement.ServiceEvents;

public record CreateUserCommand(string Username, bool IsActive, DateTime? LastLogin) : IUser;
public record UserCreatedResponse(string Username, bool IsActive, DateTime? LastLogin) : IUser;
public record DeleteUserCommand(string Username);
public record UserDeletedResponse(string Username);
public record GetUserCommand(string Username);
public record GetUserResponse(string Username, bool IsActive, DateTime? LastLogin) : IUser;
public record UpdateUserCommand(string Username, bool IsActive, DateTime? LastLogin) : IUser;
public record UserUpdatedResponse(string Username, bool IsActive, DateTime? LastLogin) : IUser;