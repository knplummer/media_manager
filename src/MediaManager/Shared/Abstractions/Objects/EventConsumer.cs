using MediaManager.Shared.Abstractions.Interfaces;

namespace MediaManager.Shared.Abstractions.Objects;

public abstract class EventConsumer() : IEventConsumer
{
    public abstract Task<Dictionary<int, string>> ValidateEventAsync<TEvent>(TEvent eventCommand) where TEvent : IEvent
    {
        throw new NotImplementedException();
    }

    public TEventResponse CreateResponse<TEventResponse>() where TEventResponse : IEventResponse, new()
    {
        throw new NotImplementedException();
    }
}
