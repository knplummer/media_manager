using MassTransit;
using MassTransit.Mediator;
using MediaManager.Infrastructure.Endpoints;
using MediaManager.Infrastructure.Validation;
using MediaManager.Shared.Domain;
using MediaManager.Features.UserManagement.Domain.ServiceCommands;
using MediaManager.Features.UserManagement.Interfaces;

namespace MediaManager.Features.UserManagement.EventConsumers;

public class CreateUserConsumer : IConsumer<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<CreateUserCommand> context)
    {
        var command = context.Message;

        var user = new User
        {
            Username = command.Username,
            IsActive = false,
            LastLogin = null,
        };

        await _userRepository.AddUserAsync(user, context.CancellationToken);

        await context.RespondAsync(user);
    }
}

public class CreateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/users", async (CreateUserCommand command, IMediator mediator) =>
        {
            var client = mediator.CreateRequestClient<CreateUserCommand>();
            var response = await client.GetResponse<User>(command);
            return Results.Created($"/api/users/{response.Message.UserId}", response.Message);
        })
        .WithTags("Users")
        .AddEndpointFilter<ValidationFilter<CreateUserCommand>>();
    }
}
