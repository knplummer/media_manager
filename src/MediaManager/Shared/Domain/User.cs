namespace MediaManager.Shared.Domain;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime LastLogin { get; set; }

    // Navigation properties for relationships
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
