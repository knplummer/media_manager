using MediaManager.Shared.Abstractions.Interfaces;

namespace MediaManager.Shared.Domain.Models;

public class AccessRequest : IAuditableEntity
{
    public int UserRequestId { get; set; }
    public string? Note { get; set; }
    public bool IsDenied { get; set; }

    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public User? Creator { get; set; }
    public User? Updater { get; set; }
}
