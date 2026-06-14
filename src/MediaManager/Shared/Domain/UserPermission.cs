namespace MediaManager.Shared.Domain;

public class UserPermission : IAuditableCreationEntity
{
    public int UserPermissionId { get; set; }
    public int UserId { get; set; }
    public int PermissionId { get; set; }

    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }

    public User? User { get; set; }
    public Permission? Permission { get; set; }
    public User? Creator { get; set; }
}
