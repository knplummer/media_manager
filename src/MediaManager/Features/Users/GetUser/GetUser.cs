using MassTransit;
using MassTransit.Mediator;
using MediaManager.Infrastructure.Endpoints;
using MediaManager.Infrastructure.Persistence;
using MediaManager.Shared.Domain;

namespace MediaManager.Features.Users.GetUser;

public record GetUserQuery(int UserId);

public class GetUserConsumer : IConsumer<GetUserQuery>
{
    private readonly MediaManagerDbContext _dbContext;

    public GetUserConsumer(MediaManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<GetUserQuery> context)
    {
        var user = await _dbContext.Users.FindAsync(new object[] { context.Message.UserId }, context.CancellationToken);
        
        if (user == null)
        {
            // For now, we can throw an exception or return a special NotFound result.
            // A more robust solution might return a Result<User> pattern.
            throw new KeyNotFoundException($"User with ID {context.Message.UserId} not found.");
        }

        await context.RespondAsync(user);
    }
}

public class GetUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users/{id:int}", async (int id, IMediator mediator) =>
        {
            try
            {
                var client = mediator.CreateRequestClient<GetUserQuery>();
                var response = await client.GetResponse<User>(new GetUserQuery(id));
                return Results.Ok(response.Message);
            }
            catch (RequestFaultException ex) when (ex.InnerException is KeyNotFoundException)
            {
                return Results.NotFound();
            }
        })
        .WithTags("Users");
    }
}
