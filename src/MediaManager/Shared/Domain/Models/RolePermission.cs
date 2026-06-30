using MediaManager.Shared.Domain;

namespace MediaManager.Shared.Domain.Models;

public class RolePermission : IAuditableCreationEntity
{
    public int RolePermissionId { get; set; }
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }

    public Role? Role { get; set; }
    public Permission? Permission { get; set; }
    public User? Creator { get; set; }
}
