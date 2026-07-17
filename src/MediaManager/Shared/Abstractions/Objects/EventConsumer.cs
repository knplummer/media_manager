using MassTransit;
using MediaManager.Shared.Abstractions.Interfaces;
using MediaManager.ErrorConstants;

namespace MediaManager.Shared.Abstractions.Objects;

public abstract class EventConsumer<TEvent>(ILogger logger) : IEventConsumer, IConsumer<TEvent> where TEvent : class, IEvent
{
    private readonly ILogger _logger = logger;
    private Dictionary<int, string> validationErrors = new Dictionary<int, string>();
    
    public abstract Task ValidateTypedEventAsync(TEvent eventCommand);
    
    public async Task<Dictionary<int, string>> ValidateEventAsync<TEventCommand>(TEventCommand eventCommand) where TEventCommand : IEvent
    {
        this.ValidateBaseEvent(eventCommand);
        await this.ValidateTypedEventAsync((eventCommand as TEvent)!);
        return validationErrors;
    }

    public TEventResponse CreateResponse<TEventResponse>(IEvent @event) where TEventResponse : IEventResponse, new()
    {
        throw new NotImplementedException();
    }

    private void ValidateBaseEvent<TEventCommand>(TEventCommand eventCommand) where TEventCommand : IEvent
    {
        if
    }

    // MassTransit entry point
    public async Task Consume(ConsumeContext<TEvent> context)
    {
        var errors = await ValidateEventAsync(context.Message);
        
        if (errors.Count > 0)
        {
            // Depending on architecture, you can throw a custom validation exception here 
            // so MassTransit routes it to the _error queue, or you can handle it explicitly.
            throw new Exception("Event validation failed.");
        }

        await ProcessEventAsync(context);
    }

    // Abstract method for derived classes to implement their specific consumption logic
    protected abstract Task ProcessEventAsync(ConsumeContext<TEvent> context);
}
