namespace MediaManager.Shared.Abstractions.Interfaces;

public interface IEventResponse
{
    bool IsSuccess { get; init; }
    Dictionary<int, string> ErrorCodes { get;  init; }
}