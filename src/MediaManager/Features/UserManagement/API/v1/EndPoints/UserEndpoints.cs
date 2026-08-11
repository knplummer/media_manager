using MassTransit;
using MediaManager.Infrastructure.Endpoints;
using MediaManager.Infrastructure.Validation;
using MediaManager.Features.UserManagement.API.v1.Messages;
using MediaManager.Features.UserManagement.ServiceEvents;
using MediaManager.Features.UserManagement.Mappers;

namespace MediaManager.Features.UserManagement.API.v1.Endpoints;


//TODO: Figure out idempodent solution look into redis caching transactions (probably a middle ware)
public class CreateUserEndpoint() : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/users", async (CreateUserMessage message, IRequestClient<CreateUserCommand> client, UserMapper userMapper) =>
        {
            var command = userMapper.MessageToCreateCommand(message, Guid.NewGuid(), "API", DateTime.UtcNow);
            var response = await client.GetResponse<UserCreatedResponse>(command);
            return Results.Created($"/api/v1/users/{response.Message.Username}", response.Message);
        })
        .WithTags("Users")
        .AddEndpointFilter<ValidationFilter<CreateUserMessage>>();
    }
}

public class UpdateUserEndpoint() : IEndpoint
{   
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/v1/users", async (UpdateUserMessage message, IRequestClient<UpdateUserCommand> client, UserMapper userMapper) =>
        {
            var command = userMapper.MessageToUpdateCommand(message, Guid.NewGuid(), "API", DateTime.UtcNow);
            var response = await client.GetResponse<UserUpdatedResponse>(command);
            return Results.Ok(response.Message);
        })
        .WithTags("Users")
        .AddEndpointFilter<ValidationFilter<UpdateUserMessage>>();
    }
}