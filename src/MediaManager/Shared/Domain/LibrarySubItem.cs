namespace MediaManager.Shared.Domain;

public class LibrarySubItem : IAuditableEntity
{
    public int LibrarySubItemId { get; set; }
    public int LibraryItemId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string OwnedFormat { get; set; } = string.Empty;
    public int Order { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Metadata { get; set; } = string.Empty;
    public int? MediaId { get; set; }

    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public LibraryItem? LibraryItem { get; set; }
    public Media? Media { get; set; }
    public User? Creator { get; set; }
    public User? Updater { get; set; }
}
