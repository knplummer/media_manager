# 06. Event-Driven Communication

## Overview
To enforce strict boundaries within the Vertical Slice Architecture, feature slices must not communicate with each other directly via method calls or direct class instantiation. Instead, the application relies on an **Event-Driven Architecture (EDA)** powered by **MassTransit**.

## Implementation Guidelines

### 1. Integration Events
* Define events as pure C# `record` types.
* Events must represent something that has *already happened* (e.g., `MediaUploadedEvent`, `BucketCreatedEvent`).
* Keep event payloads small and flat. Pass identifiers (`MediaId`) rather than entire entity objects.

### 2. Publishing Events
* Handlers that cause side effects or require secondary processing must publish an event.
* Inject `IPublishEndpoint` (MassTransit) into the handler to publish events asynchronously.

### 3. Consuming Events
* Slices that need to react to an event must define a class implementing `IConsumer<TEvent>`.
* Consumers handle the event independently. If a consumer fails, it should not roll back the primary transaction of the publisher.

## Configuration & Broker
* **In-Memory Transport:** By default, the application is configured to use MassTransit's In-Memory transport for single-container execution.
* **Extensibility:** The consumers and publishers require no code changes if the underlying transport is upgraded to RabbitMQ or Azure Service Bus for distributed scaling.

## Code Example

**Publisher (Inside Media Upload Slice):**
```csharp
public async Task<UploadMediaResponse> Handle(UploadMediaCommand request, CancellationToken cancellationToken)
{
    // 1. Perform primary DB logic
    var media = new Media { /* ... */ };
    await _dbContext.SaveChangesAsync(cancellationToken);

    // 2. Publish event via MassTransit
    await _publishEndpoint.Publish(new MediaIngestedEvent(media.MediaId, media.FileSize), cancellationToken);

    return new UploadMediaResponse(media.MediaId);
}
```

**Consumer (Inside Audit Logging Slice):**
```csharp
public class LogAuditOnMediaIngestConsumer : IConsumer<MediaIngestedEvent>
{
    public async Task Consume(ConsumeContext<MediaIngestedEvent> context)
    {
        var mediaId = context.Message.MediaId;
        
        // Write to history table
        _dbContext.MediaHistory.Add(new MediaHistory { Action = "UPLOAD", MediaId = mediaId });
        await _dbContext.SaveChangesAsync(context.CancellationToken);
    }
}
```
