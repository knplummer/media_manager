using MassTransit.Mediator;
using MediaManager.Infrastructure.Endpoints;
using MediaManager.Infrastructure.Validation;
using MediaManager.Features.UserManagement.API.v1.Messages;
using MediaManager.Features.UserManagement.ServiceEvents;
using MediaManager.Features.UserManagement.Mappers;

namespace MediaManager.Features.UserManagement.API.v1.Endpoints;


//TODO: Figure out idempodent solution look into redis caching transactions (probably a middle ware)
public class CreateUserEndpoint(UserMapper userMapper) : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/users", async (CreateUserMessage message, IMediator mediator) =>
        {
            var client = mediator.CreateRequestClient<CreateUserCommand>();
            var response = await client.GetResponse<UserCreatedResponse>(userMapper.MessageToCommand<CreateUserCommand>(message));
            return Results.Created($"/api/v1/users/{response.Message.Username}", response.Message);
        })
        .WithTags("Users")
        .AddEndpointFilter<ValidationFilter<CreateUserMessage>>();
    }
}

public class UpdateUserEndpoint(UserMapper userMapper) : IEndpoint
{   
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/v1/users", async (UpdateUserMessage message, IMediator mediator) =>
        {
            var client = mediator.CreateRequestClient<UpdateUserCommand>();
            var response = await client.GetResponse<UserUpdatedResponse>(userMapper.MessageToCommand<UpdateUserCommand>(message));
            return Results.Ok(response.Message);
        })
        .WithTags("Users")
        .AddEndpointFilter<ValidationFilter<UpdateUserMessage>>();
    }
}