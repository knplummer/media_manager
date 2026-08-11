using MassTransit;

using MediaManager.ErrorConstants;
using MediaManager.Shared.Domain.Constants;
using MediaManager.Shared.Abstractions.Objects;

using MediaManager.Features.UserManagement.ServiceEvents;
using MediaManager.Features.UserManagement.Abstractions.Interfaces;
using MediaManager.Features.UserManagement.Mappers;

namespace MediaManager.Features.UserManagement.EventProcessors;


//Validate command against database before processing
public class CreateUserConsumer(IUserManagementRepository userRepository, IUserManagementValidationRepository validationRepository, UserMapper mapper, ILogger<CreateUserConsumer> logger) : EventConsumer<CreateUserCommand>(logger)
{
    public override async Task Consume(ConsumeContext<CreateUserCommand> context)
    {
        var eventId = context.Message.Id;
        try{
            var command = context.Message;

            var validationResult = await this.ValidateEventAsync(command);

            if (validationResult != null)
            {
                await context.RespondAsync(new UserCreatedResponse(Id: command.Id, Source: InternalSources.CommandProcessing, Timestamp: DateTime.UtcNow, Username: string.Empty, IsActive: false, LastLogin: null, IsSuccess: false, ErrorCodes: validationResult));
                return;
            }

            var user = mapper.ObjectToEntity(command);

            await userRepository.AddUserAsync(user, context.CancellationToken);

            var mappedResponse = mapper.EntityToCreatedResponse(user, command.Id, InternalSources.CommandProcessing, DateTime.UtcNow, true, null);
            await context.RespondAsync(mappedResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing CreateUserCommand.");
            await context.RespondAsync(new UserCreatedResponse(Id: eventId, Source: InternalSources.ErrorHandling, Timestamp: DateTime.UtcNow, Username: string.Empty, IsActive: false, LastLogin: null, IsSuccess: false, ErrorCodes: ErrorResponseCodes.InternalErrorRepsonse));
        }
    }

    public override async Task ValidateTypedEventAsync(CreateUserCommand eventCommand)
    {
        if(await validationRepository.UserExistsAsync(eventCommand.Username))
        {
            validationErrors.Add(ErrorResponseCodes.UserAlreadyExists.ToResponseCode());
        }
    }
}
