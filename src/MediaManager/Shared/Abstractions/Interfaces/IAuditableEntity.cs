namespace MediaManager.Shared.Abstractions.Interfaces;

public interface IAuditableEntity
{
    int CreatedBy { get; set; }
    DateTime CreatedDate { get; set; }
    int? UpdatedBy { get; set; }
    DateTime? UpdatedDate { get; set; }
}

public interface IAuditableCreationEntity
{
    int CreatedBy { get; set; }
    DateTime CreatedDate { get; set; }
}
