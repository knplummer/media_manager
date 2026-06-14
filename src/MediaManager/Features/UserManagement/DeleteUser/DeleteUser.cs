using MassTransit;
using MassTransit.Mediator;
using MediaManager.Infrastructure.Endpoints;
using MediaManager.Infrastructure.Persistence;
using MediaManager.Shared.Domain;

namespace MediaManager.Features.UserManagement.DeleteUser;

public record DeleteUserCommand(int UserId);
public record DeleteUserResponse(bool Success);

public class DeleteUserConsumer : IConsumer<DeleteUserCommand>
{
    private readonly MediaManagerDbContext _dbContext;

    public DeleteUserConsumer(MediaManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<DeleteUserCommand> context)
    {
        var command = context.Message;
        var user = await _dbContext.Users.FindAsync(new object[] { command.UserId }, context.CancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {command.UserId} not found.");
        }

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync(context.CancellationToken);

        await context.RespondAsync(new DeleteUserResponse(true));
    }
}

public class DeleteUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/users/{id:int}", async (int id, IMediator mediator) =>
        {
            try
            {
                var client = mediator.CreateRequestClient<DeleteUserCommand>();
                await client.GetResponse<DeleteUserResponse>(new DeleteUserCommand(id));
                return Results.NoContent();
            }
            catch (RequestFaultException)
            {
                return Results.NotFound();
            }
        })
        .WithTags("Users");
    }
}
