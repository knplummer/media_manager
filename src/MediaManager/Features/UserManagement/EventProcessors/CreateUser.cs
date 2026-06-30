using MassTransit;

using MediaManager.Shared.Domain.Models;
using MediaManager.Features.UserManagement.ServiceEvents;
using MediaManager.Features.UserManagement.Abstractions.Interfaces;

namespace MediaManager.Features.UserManagement.EventProcessors;


//Validate command against database before processing
public class CreateUserConsumer(IUserRepository userRepository) : IConsumer<CreateUserCommand>
{
    public async Task Consume(ConsumeContext<CreateUserCommand> context)
    {
        var command = context.Message;

        var user = new User
        {
            Username = command.Username,
            IsActive = command.IsActive,
            LastLogin = command.LastLogin,
        };

        await userRepository.AddUserAsync(user, context.CancellationToken);

        await context.RespondAsync(user);
    }
}

