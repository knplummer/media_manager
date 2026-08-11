using MediaManager.Shared.Abstractions.Interfaces;
using MediaManager.Features.UserManagement.Abstractions.Interfaces;

namespace MediaManager.Features.UserManagement.ServiceEvents;

public record CreateUserCommand(Guid Id, string Source, DateTime Timestamp, string Username, bool IsActive, DateTime? LastLogin) : IUser, IEvent;
public record UserCreatedResponse(Guid Id, string Source, DateTime Timestamp, string Username, bool IsActive, DateTime? LastLogin, bool IsSuccess, Dictionary<int, string>? ErrorCodes) : IUser, IEvent, IEventResponse;
public record DeleteUserCommand(Guid Id, string Source, DateTime Timestamp, string Username) : IEvent;
public record UserDeletedResponse(Guid Id, string Source, DateTime Timestamp, string Username, bool IsSuccess, Dictionary<int, string>? ErrorCodes) : IEvent, IEventResponse;
public record GetUserCommand(Guid Id, string Source, DateTime Timestamp, string Username) : IEvent;
public record GetUserResponse(Guid Id, string Source, DateTime Timestamp, string Username, bool IsActive, DateTime? LastLogin, bool IsSuccess, Dictionary<int, string>? ErrorCodes) : IUser, IEvent, IEventResponse;
public record UpdateUserCommand(Guid Id, string Source, DateTime Timestamp, string Username, bool IsActive, DateTime? LastLogin) : IUser, IEvent;
public record UserUpdatedResponse(Guid Id, string Source, DateTime Timestamp, string Username, bool IsActive, DateTime? LastLogin, bool IsSuccess, Dictionary<int, string>? ErrorCodes) : IUser, IEvent, IEventResponse;