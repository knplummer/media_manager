using MediaManager.Shared.Domain;

namespace MediaManager.Shared.Domain.Models;

public class UserRole : IAuditableCreationEntity
{
    public int UserRoleId { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }

    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }

    public User? User { get; set; }
    public Role? Role { get; set; }
    public User? Creator { get; set; }
}
