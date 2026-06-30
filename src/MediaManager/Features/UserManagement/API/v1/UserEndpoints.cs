using MassTransit.Mediator;
using MediaManager.Infrastructure.Endpoints;
using MediaManager.Infrastructure.Validation;
using MediaManager.Features.UserManagement.API.v1.Messages;
using MediaManager.Features.UserManagement.ServiceEvents;

namespace MediaManager.Features.UserManagement.API.v1;

public class CreateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/users", async (CreateUserMessage command, IMediator mediator) =>
        {
            var client = mediator.CreateRequestClient<CreateUserCommand>();
            var response = await client.GetResponse<UserCreatedResponse>(command);
            return Results.Created($"/api/v1/users/{response.Message.Username}", response.Message);
        })
        .WithTags("Users")
        .AddEndpointFilter<ValidationFilter<CreateUserMessage>>();
    }
}