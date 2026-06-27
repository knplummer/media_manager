namespace MediaManager.Shared.Domain;

public class Role : IAuditableEntity
{
    public int RoleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public User? Creator { get; set; }
    public User? Updater { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
