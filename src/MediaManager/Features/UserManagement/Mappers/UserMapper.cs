using Riok.Mapperly.Abstractions;
using MediaManager.Shared.Domain.Models;
using MediaManager.Features.UserManagement.Abstractions.Interfaces;
using MediaManager.Features.UserManagement.API.v1.Messages;
using MediaManager.Features.UserManagement.ServiceEvents;

namespace MediaManager.Features.UserManagement.Mappers;

[Mapper]
public partial class UserMapper
{
    // ════════════════════════════════════════════════════════════════
    // 1. ObjectToEntity: IUser command → User entity
    //    Source has all needed props; no extra params required.
    //    Usage: mapper.ObjectToEntity(command)  (TCommand is inferred)
    // ════════════════════════════════════════════════════════════════
    public partial User ObjectToEntity<TCommand>(TCommand command)
        where TCommand : IUser;

    [MapperIgnoreTarget(nameof(User.UserId))]
    [MapperIgnoreTarget(nameof(User.CreatedBy))]
    [MapperIgnoreTarget(nameof(User.CreatedDate))]
    [MapperIgnoreTarget(nameof(User.UpdatedBy))]
    [MapperIgnoreTarget(nameof(User.UpdatedDate))]
    [MapperIgnoreTarget(nameof(User.Creator))]
    [MapperIgnoreTarget(nameof(User.Updater))]
    [MapperIgnoreTarget(nameof(User.UserPermissions))]
    [MapperIgnoreTarget(nameof(User.UserRoles))]
    [MapperIgnoreSource(nameof(CreateUserCommand.Id))]
    [MapperIgnoreSource(nameof(CreateUserCommand.Source))]
    [MapperIgnoreSource(nameof(CreateUserCommand.Timestamp))]
    private partial User MapCreateUserCommandToUser(CreateUserCommand command);

    [MapperIgnoreTarget(nameof(User.UserId))]
    [MapperIgnoreTarget(nameof(User.CreatedBy))]
    [MapperIgnoreTarget(nameof(User.CreatedDate))]
    [MapperIgnoreTarget(nameof(User.UpdatedBy))]
    [MapperIgnoreTarget(nameof(User.UpdatedDate))]
    [MapperIgnoreTarget(nameof(User.Creator))]
    [MapperIgnoreTarget(nameof(User.Updater))]
    [MapperIgnoreTarget(nameof(User.UserPermissions))]
    [MapperIgnoreTarget(nameof(User.UserRoles))]
    [MapperIgnoreSource(nameof(UpdateUserCommand.Id))]
    [MapperIgnoreSource(nameof(UpdateUserCommand.Source))]
    [MapperIgnoreSource(nameof(UpdateUserCommand.Timestamp))]
    private partial User MapUpdateUserCommandToUser(UpdateUserCommand command);

    // ════════════════════════════════════════════════════════════════
    // 2. EntityToObject: User entity → response records
    //    Responses need Id, Source, Timestamp, IsSuccess, ErrorCodes
    //    that User doesn't have — supplied via additional parameters.
    //    NOTE: Generic dispatch is not supported with additional
    //    parameters in Mapperly, so each response type gets its own
    //    public method. Callers use the concrete method directly.
    // ════════════════════════════════════════════════════════════════
    [MapperIgnoreSource(nameof(User.UserId))]
    [MapperIgnoreSource(nameof(User.CreatedBy))]
    [MapperIgnoreSource(nameof(User.CreatedDate))]
    [MapperIgnoreSource(nameof(User.UpdatedBy))]
    [MapperIgnoreSource(nameof(User.UpdatedDate))]
    [MapperIgnoreSource(nameof(User.Creator))]
    [MapperIgnoreSource(nameof(User.Updater))]
    [MapperIgnoreSource(nameof(User.UserPermissions))]
    [MapperIgnoreSource(nameof(User.UserRoles))]
    public partial UserCreatedResponse EntityToCreatedResponse(
        User user, Guid id, string source, DateTime timestamp,
        bool isSuccess, Dictionary<int, string>? errorCodes);

    [MapperIgnoreSource(nameof(User.UserId))]
    [MapperIgnoreSource(nameof(User.CreatedBy))]
    [MapperIgnoreSource(nameof(User.CreatedDate))]
    [MapperIgnoreSource(nameof(User.UpdatedBy))]
    [MapperIgnoreSource(nameof(User.UpdatedDate))]
    [MapperIgnoreSource(nameof(User.Creator))]
    [MapperIgnoreSource(nameof(User.Updater))]
    [MapperIgnoreSource(nameof(User.UserPermissions))]
    [MapperIgnoreSource(nameof(User.UserRoles))]
    public partial GetUserResponse EntityToGetResponse(
        User user, Guid id, string source, DateTime timestamp,
        bool isSuccess, Dictionary<int, string>? errorCodes);

    [MapperIgnoreSource(nameof(User.UserId))]
    [MapperIgnoreSource(nameof(User.CreatedBy))]
    [MapperIgnoreSource(nameof(User.CreatedDate))]
    [MapperIgnoreSource(nameof(User.UpdatedBy))]
    [MapperIgnoreSource(nameof(User.UpdatedDate))]
    [MapperIgnoreSource(nameof(User.Creator))]
    [MapperIgnoreSource(nameof(User.Updater))]
    [MapperIgnoreSource(nameof(User.UserPermissions))]
    [MapperIgnoreSource(nameof(User.UserRoles))]
    public partial UserUpdatedResponse EntityToUpdatedResponse(
        User user, Guid id, string source, DateTime timestamp,
        bool isSuccess, Dictionary<int, string>? errorCodes);

    // ════════════════════════════════════════════════════════════════
    // 3. MessageToCommand: API message → service command
    //    Commands need Id, Source, Timestamp from IEvent —
    //    supplied via additional parameters.
    //    NOTE: Same limitation as above — concrete methods per type.
    // ════════════════════════════════════════════════════════════════
    public partial CreateUserCommand MessageToCreateCommand(
        CreateUserMessage message, Guid id, string source, DateTime timestamp);

    public partial UpdateUserCommand MessageToUpdateCommand(
        UpdateUserMessage message, Guid id, string source, DateTime timestamp);
}

