namespace MediaManager.Features.UserManagement.Records;

public abstract record UserRecord(string Username);

public record CreateUser(string Username) : UserRecord(Username);
public record DeleteUser(string Username) : UserRecord(Username);
public record GetUser(string Username) : UserRecord(Username);
public record GetUsers();
public record UpdateUser(string Username, bool IsActive) : UserRecord(Username);