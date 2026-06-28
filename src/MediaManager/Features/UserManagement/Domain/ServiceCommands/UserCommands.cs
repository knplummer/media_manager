using MediaManager.Features.UserManagement.Domain.Abstractions;

namespace MediaManager.Features.UserManagement.Domain.ServiceCommands;


public record CreateUserCommand(string Username) : UserRecord(Username);
public record DeleteUserCommand(string Username) : UserRecord(Username);
public record GetUserCommand(string Username) : UserRecord(Username);
public record GetUsersCommand(int? PageNumber = null, int? PageSize = null);
public record UpdateUserCommand(string Username, bool IsActive) : UserRecord(Username);