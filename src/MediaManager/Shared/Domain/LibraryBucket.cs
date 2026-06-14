namespace MediaManager.Shared.Domain;

public class LibraryBucket : IAuditableEntity
{
    public int LibraryBucketId { get; set; }
    public string GroupType { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string? Metadata { get; set; }

    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public User? Creator { get; set; }
    public User? Updater { get; set; }

    public ICollection<LibraryItem> LibraryItems { get; set; } = new List<LibraryItem>();
}
