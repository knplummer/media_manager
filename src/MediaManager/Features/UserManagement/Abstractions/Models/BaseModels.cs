namespace MediaManager.Features.UserManagement.Abstractions.Models;

public abstract record UserRecord(string Username, bool IsActive, DateTime? LastLogin);