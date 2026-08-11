using MassTransit;
using MediaManager.Shared.Abstractions.Interfaces;
using MediaManager.ErrorConstants;

namespace MediaManager.Shared.Abstractions.Objects;

public abstract class EventConsumer<TEvent>(ILogger logger) : IEventConsumer, IConsumer<TEvent> where TEvent : class, IEvent
{
    protected readonly ILogger _logger = logger;
    protected List<KeyValuePair<int, string>> validationErrors = new List<KeyValuePair<int, string>>();
    
    public abstract Task ValidateTypedEventAsync(TEvent eventCommand);
    
    public async Task<Dictionary<int, string>?> ValidateEventAsync<TEventCommand>(TEventCommand eventCommand) where TEventCommand : IEvent
    {
        try
        {
            this.ValidateBaseEvent(eventCommand);
            await this.ValidateTypedEventAsync((eventCommand as TEvent)!);
            if (!validationErrors.Any())
            {
                return null;
            }
            return validationErrors.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while validating event.");
            return ErrorResponseCodes.InternalErrorRepsonse;
        }
    }


    private void ValidateBaseEvent<TEventCommand>(TEventCommand eventCommand) where TEventCommand : IEvent
    {
        if(eventCommand.Id == Guid.Empty)
        {
            validationErrors.Add(ErrorResponseCodes.MissingEventId.ToResponseCode());
        }

        if(string.IsNullOrWhiteSpace(eventCommand.Source))
        {
            validationErrors.Add(ErrorResponseCodes.MissingSource.ToResponseCode());
        }

        if(eventCommand.Timestamp == default)
        {
            validationErrors.Add(ErrorResponseCodes.MissingTimestamp.ToResponseCode());
        }
    }

    public abstract Task Consume(ConsumeContext<TEvent> context);
}
