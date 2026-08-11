namespace MediaManager.Shared.Abstractions.Interfaces;

public interface IEventConsumer 
{
    Task<Dictionary<int, string>?> ValidateEventAsync<TEvent>(TEvent eventCommand) where TEvent : IEvent;
}