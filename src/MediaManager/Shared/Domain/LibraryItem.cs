namespace MediaManager.Shared.Domain;

public class LibraryItem : IAuditableEntity
{
    public int LibraryItemId { get; set; }
    public int? LibraryBucketId { get; set; }
    public string ItemType { get; set; } = string.Empty;
    public string? OwnedFormat { get; set; }
    public int? Order { get; set; }
    public string? Name { get; set; }
    public int? Year { get; set; }
    public string? Metadata { get; set; }
    public int? MediaId { get; set; }

    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public LibraryBucket? LibraryBucket { get; set; }
    public Media? Media { get; set; }
    public User? Creator { get; set; }
    public User? Updater { get; set; }

    public ICollection<LibrarySubItem> LibrarySubItems { get; set; } = new List<LibrarySubItem>();
}
