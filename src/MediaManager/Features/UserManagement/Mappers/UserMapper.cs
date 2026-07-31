using Riok.Mapperly.Abstractions;
using MediaManager.Shared.Domain.Models;
using MediaManager.Features.UserManagement.Abstractions.Interfaces;

namespace MediaManager.Features.UserManagement.Mappers;

[Mapper]
public partial class UserManagementMapper
{
    // ServiceCommand → User: ignore entity-only properties (audit, PK, navigation)
    [MapperIgnoreTarget(nameof(User.UserId))]
    [MapperIgnoreTarget(nameof(User.CreatedBy))]
    [MapperIgnoreTarget(nameof(User.CreatedDate))] 
    [MapperIgnoreTarget(nameof(User.UpdatedBy))]
    [MapperIgnoreTarget(nameof(User.UpdatedDate))]
    [MapperIgnoreTarget(nameof(User.Creator))]
    [MapperIgnoreTarget(nameof(User.Updater))]
    [MapperIgnoreTarget(nameof(User.UserPermissions))]
    [MapperIgnoreTarget(nameof(User.UserRoles))]
    public partial User ObjectToEntity<TCommand>(TCommand command) where TCommand : IUser;

    // User → ServiceResponse: ignore entity-only source properties
    [MapperIgnoreSource(nameof(User.UserId))]
    [MapperIgnoreSource(nameof(User.CreatedBy))]
    [MapperIgnoreSource(nameof(User.CreatedDate))]
    [MapperIgnoreSource(nameof(User.UpdatedBy))]
    [MapperIgnoreSource(nameof(User.UpdatedDate))]
    [MapperIgnoreSource(nameof(User.Creator))]
    [MapperIgnoreSource(nameof(User.Updater))]
    [MapperIgnoreSource(nameof(User.UserPermissions))]
    [MapperIgnoreSource(nameof(User.UserRoles))]
    public partial TUser EntityToObject<TUser>(User user) where TUser : IUser;

    // Inbound API Message → Command: Both implement IUser and should map 1 to 1
    public partial TUser MessageToCommand<TUser>(IUser user) where TUser : IUser;

    //Can I make a mapper for a response object that auto populates the common response properties like success, message, and status code? I want to avoid having to set those properties in every response object manually.

}
