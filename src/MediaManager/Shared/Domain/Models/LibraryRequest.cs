using MediaManager.Shared.Abstractions.Interfaces;

namespace MediaManager.Shared.Domain.Models;

public class LibraryRequest : IAuditableEntity
{
    public int LibraryRequestId { get; set; }
    public string RequestTitle { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? RelevantLink { get; set; }
    public string? Note { get; set; }
    public int Votes { get; set; }
    public bool IsAccepted { get; set; }

    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public User? Creator { get; set; }
    public User? Updater { get; set; }
}
