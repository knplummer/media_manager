using FluentValidation;
using MassTransit;
using MassTransit.Mediator;
using MediaManager.Infrastructure.Endpoints;
using MediaManager.Infrastructure.Persistence;
using MediaManager.Infrastructure.Validation;
using MediaManager.Shared.Domain;

namespace MediaManager.Features.UserManagement.UpdateUser;

public record UpdateUserCommand(int UserId, string Username, bool IsActive);

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username cannot be empty.")
            .MaximumLength(25).WithMessage("Username cannot exceed 25 characters.");
    }
}

public class UpdateUserConsumer : IConsumer<UpdateUserCommand>
{
    private readonly MediaManagerDbContext _dbContext;

    public UpdateUserConsumer(MediaManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<UpdateUserCommand> context)
    {
        var command = context.Message;
        var user = await _dbContext.Users.FindAsync(new object[] { command.UserId }, context.CancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {command.UserId} not found.");
        }

        user.Username = command.Username;
        user.IsActive = command.IsActive;

        await _dbContext.SaveChangesAsync(context.CancellationToken);

        await context.RespondAsync(user);
    }
}

public class UpdateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/{id:int}", async (int id, UpdateUserCommand command, IMediator mediator) =>
        {
            if (id != command.UserId)
            {
                return Results.BadRequest("Route ID and body ID must match.");
            }

            try
            {
                var client = mediator.CreateRequestClient<UpdateUserCommand>();
                var response = await client.GetResponse<User>(command);
                return Results.Ok(response.Message);
            }
            catch (RequestFaultException)
            {
                return Results.NotFound();
            }
        })
        .WithTags("Users")
        .AddEndpointFilter<ValidationFilter<UpdateUserCommand>>();
    }
}
