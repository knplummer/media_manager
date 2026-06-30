using MediaManager.Features.UserManagement.Abstractions.Models;

namespace MediaManager.Features.UserManagement.API.v1.Messages;


public record CreateUserMessage(string Username, bool IsActive, DateTime? LastLogin) : UserRecord(Username, IsActive, LastLogin);
public record DeleteUserMessage(string Username) : UserRecord(Username, false, null);
public record GetUserMessage(string Username) : UserRecord(Username, false, null);
public record UpdateUserMessage(string Username, bool IsActive, DateTime? LastLogin) : UserRecord(Username, IsActive, LastLogin);