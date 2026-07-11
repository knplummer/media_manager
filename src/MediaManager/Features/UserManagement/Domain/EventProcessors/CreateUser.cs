using MassTransit;

using MediaManager.Shared.Abstractions.Interfaces;
using MediaManager.Shared.Domain.Enums;

using MediaManager.Features.UserManagement.ServiceEvents;
using MediaManager.Features.UserManagement.Abstractions.Interfaces;
using MediaManager.Features.UserManagement.Mappers;

namespace MediaManager.Features.UserManagement.EventProcessors;


//Validate command against database before processing
public class CreateUserConsumer(IUserRepository userRepository, UserMapper mapper) : IConsumer<CreateUserCommand>, IEventConsumer
{
    public async Task Consume(ConsumeContext<CreateUserCommand> context)
    {
        var transactionId = context.Message.Id;
        try{
            var command = context.Message;

            var validationResult = await ValidateEventAsync(command);

            if (!validationResult.Any())
            {
                await context.RespondAsync(new UserCreatedResponse(Id: command.Id, Source: InternalSources.CommandProcessing.ToString(), Timestamp: DateTime.UtcNow, Username: string.Empty, IsActive: false, LastLogin: null, IsSuccess: false, ErrorCodes: validationResult));
                return;
            }

            var user = mapper.ObjectToEntity(command);

            await userRepository.AddUserAsync(user, context.CancellationToken);

            var mappedResponse = mapper.EntityToObject<UserCreatedResponse>(user);
            var response = mappedResponse with { IsSuccess = true };
            await context.RespondAsync(response);
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during processing
            await context.RespondAsync(new UserCreatedResponse(Id: transactionId, Source: InternalSources.ErrorHandling.ToString(), Timestamp: DateTime.UtcNow, Username: string.Empty, IsActive: false, LastLogin: null, IsSuccess: false, ErrorCodes: new Dictionary<int, string> { { 0, ex.Message } }));
        }
    }

    public Task<Dictionary<int, string>> ValidateEventAsync<TEvent>(TEvent eventCommand) where TEvent : IEvent
    {
        throw new NotImplementedException();
    }

}
