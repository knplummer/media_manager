using MediaManager.Features.UserManagement.Records.Abstractions;

namespace MediaManager.Features.UserManagement.Records.InboundMessages;


public record CreateUser(string Username) : UserRecord(Username);
public record DeleteUser(string Username) : UserRecord(Username);
public record GetUser(string Username) : UserRecord(Username);
public record GetUsers(int? PageNumber = null, int? PageSize = null);
public record UpdateUser(string Username, bool IsActive) : UserRecord(Username);