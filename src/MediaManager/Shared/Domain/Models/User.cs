using MediaManager.Shared.Domain;

namespace MediaManager.Shared.Domain.Models;

public class User : IAuditableEntity
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime? LastLogin { get; set; }

    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public User? Creator { get; set; }
    public User? Updater { get; set; }

    // Navigation properties for relationships
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
