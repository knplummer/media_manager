using MassTransit;
using MassTransit.Mediator;
using MediaManager.Infrastructure.Endpoints;
using MediaManager.Infrastructure.Persistence;
using MediaManager.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.Features.UserManagement.GetUsers;

public record GetUsersQuery();
public record GetUsersResponse(IEnumerable<User> Users);

public class GetUsersConsumer : IConsumer<GetUsersQuery>
{
    private readonly MediaManagerDbContext _dbContext;

    public GetUsersConsumer(MediaManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<GetUsersQuery> context)
    {
        var users = await _dbContext.Users.ToListAsync(context.CancellationToken);
        await context.RespondAsync(new GetUsersResponse(users));
    }
}

public class GetUsersEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users", async (IMediator mediator) =>
        {
            var client = mediator.CreateRequestClient<GetUsersQuery>();
            var response = await client.GetResponse<GetUsersResponse>(new GetUsersQuery());
            return Results.Ok(response.Message.Users);
        })
        .WithTags("Users");
    }
}
