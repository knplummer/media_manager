namespace MediaManager.Shared.Abstractions.Interfaces;

public interface IEvent
{
    Guid Id { get; init; }
    string Source { get; init; }
    DateTime Timestamp { get; init; }
}