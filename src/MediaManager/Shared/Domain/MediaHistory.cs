namespace MediaManager.Shared.Domain;

public class MediaHistory
{
    public int MediaHistoryId { get; set; }
    public int MediaId { get; set; }
    public string? StorageGuid { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ActionUser { get; set; }
    public DateTime ActionTime { get; set; }

    public Media? Media { get; set; }
    public User? User { get; set; }
}
