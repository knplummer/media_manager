using MediaManager.Features.UserManagement.Abstractions.Interfaces;

namespace MediaManager.Features.UserManagement.API.v1.Messages;


public record CreateUserMessage(string Username, bool IsActive, DateTime? LastLogin) : IUser;
public record DeleteUserMessage(string Username);
public record GetUserMessage(string Username);
public record UpdateUserMessage(string Username, bool IsActive, DateTime? LastLogin) : IUser;