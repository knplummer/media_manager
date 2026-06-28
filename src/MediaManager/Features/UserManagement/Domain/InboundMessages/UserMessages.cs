using MediaManager.Features.UserManagement.Domain.Abstractions;

namespace MediaManager.Features.UserManagement.Domain.InboundMessages;


public record CreateUserMessage(string Username) : UserRecord(Username);
public record DeleteUserMessage(string Username) : UserRecord(Username);
public record GetUserMessage(string Username) : UserRecord(Username);
public record GetUsersMessage(int? PageNumber = null, int? PageSize = null);
public record UpdateUserMessage(string Username, bool IsActive) : UserRecord(Username);