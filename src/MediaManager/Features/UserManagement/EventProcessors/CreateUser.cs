using MassTransit;

using MediaManager.Features.UserManagement.ServiceEvents;
using MediaManager.Features.UserManagement.Abstractions.Interfaces;
using MediaManager.Features.UserManagement.Mappers;

namespace MediaManager.Features.UserManagement.EventProcessors;


//Validate command against database before processing
public class CreateUserConsumer(IUserRepository userRepository, UserMapper mapper) : IConsumer<CreateUserCommand>
{
    public async Task Consume(ConsumeContext<CreateUserCommand> context)
    {
        var command = context.Message;

        var user = mapper.ObjectToEntity(command);

        await userRepository.AddUserAsync(user, context.CancellationToken);

        await context.RespondAsync(mapper.EntityToObject<UserCreatedResponse>(user));
    }
}
