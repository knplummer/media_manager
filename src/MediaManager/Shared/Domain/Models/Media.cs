using MediaManager.Shared.Domain;

namespace MediaManager.Shared.Domain.Models;

public class Media : IAuditableEntity
{
    public int MediaId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public bool IsAlwaysAvailable { get; set; }
    public bool IsOnMediaServer { get; set; }
    public string? ExternalStorageKey { get; set; }
    public string? StorageGuid { get; set; }

    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public User? Creator { get; set; }
    public User? Updater { get; set; }

    public ICollection<MediaHistory> History { get; set; } = new List<MediaHistory>();
    public ICollection<LibraryItem> LibraryItems { get; set; } = new List<LibraryItem>();
    public ICollection<LibrarySubItem> LibrarySubItems { get; set; } = new List<LibrarySubItem>();
}
