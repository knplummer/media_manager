using FluentValidation;
using MassTransit;
using MassTransit.Mediator;
using MediaManager.Infrastructure.Endpoints;
using MediaManager.Infrastructure.Persistence;
using MediaManager.Infrastructure.Validation;
using MediaManager.Shared.Domain;

namespace MediaManager.Features.Users.CreateUser;

public record CreateUserCommand(string Username, bool IsActive);

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username cannot be empty.")
            .MaximumLength(25).WithMessage("Username cannot exceed 25 characters.");
    }
}

public class CreateUserConsumer : IConsumer<CreateUserCommand>
{
    private readonly MediaManagerDbContext _dbContext;

    public CreateUserConsumer(MediaManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<CreateUserCommand> context)
    {
        var command = context.Message;

        var user = new User
        {
            Username = command.Username,
            IsActive = command.IsActive,
            LastLogin = DateTime.UtcNow
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(context.CancellationToken);

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
