# 02. Backend Architecture

## Overview
The Media Manager backend utilizes a **Clean Vertical Slice Architecture**. Code is organized by business feature capabilities (slices) rather than technical layers (Controllers, Services, Repositories). To ensure high testability, Clean Architecture principles (Dependency Injection and boundary isolation) are strictly enforced *within* each slice.

## Directory Structure
The application structure is strictly divided into feature slices, shared domain models, and shared infrastructure.

```text
src/
  └── MediaManager.Api/
        ├── Features/
        │     ├── LibraryBuckets/
        │     │     ├── CreateBucket.cs      # Contains Endpoint, Validator, Handler, Response DTO
        │     │     ├── GetBuckets.cs
        │     │     └── DeleteBucket.cs
        │     └── Media/
        │           ├── UploadMedia.cs
        │           └── GetMediaStream.cs
        ├── Shared/
        │     ├── Domain/                    # Shared entities (EF Core models)
        │     └── Events/                    # Shared Integration/Domain events
        ├── Infrastructure/                  # Shared services (DbContext, S3 Clients, Auth)
        └── Program.cs                       
```

## Slice Implementation Guidelines

When implementing a new feature (e.g., "Create a Library Bucket"), all logic for that feature must reside in a single file or a cohesive directory. A complete slice must separate concerns into the following components:

1. **The Endpoint (Presentation):** Responsible for HTTP routing, authorization requirements, and mapping HTTP requests to internal Commands/Queries.
2. **The Command/Query (Request DTO):** A C# `record` representing the input data payload.
3. **The Validator:** A FluentValidation rule set verifying the input payload before execution.
4. **The Handler (Business Logic):** The class containing the core logic. Handlers **must** use Dependency Injection. Do not instantiate concrete external services (`HttpClient`, S3 clients, etc.) manually. Inject interfaces (e.g., `ICloudStorageService`, `MediaManagerDbContext`).
5. **The Response (Response DTO):** A C# `record` returning data to the client.

### Cross-Slice Communication
* Slices must **never** call or instantiate another slice's handler directly. 
* To share logic or trigger secondary actions, slices must use the Event-Driven Architecture defined in the Event Communication guide.

## Example: Vertical Slice Definition
```csharp
namespace MediaManager.Api.Features.LibraryBuckets;

// 1. Endpoint
public class CreateBucketEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/buckets", async (CreateBucketCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Created($"/api/buckets/{result.Id}", result);
        })
        .WithName("CreateBucket")
        .WithTags("LibraryBuckets")
        .RequireAuthorization("ManageCatalog");
    }
}

// 2. Command
public record CreateBucketCommand(string Name, string GroupType, int? Year) : IRequest<CreateBucketResponse>;

// 3. Validator
public class CreateBucketValidator : AbstractValidator<CreateBucketCommand>
{
    public CreateBucketValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
        RuleFor(x => x.GroupType).NotEmpty().MaximumLength(25);
    }
}

// 4. Handler
public class CreateBucketHandler : IRequestHandler<CreateBucketCommand, CreateBucketResponse>
{
    private readonly MediaManagerDbContext _dbContext;

    public CreateBucketHandler(MediaManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateBucketResponse> Handle(CreateBucketCommand request, CancellationToken cancellationToken)
    {
        var bucket = new LibraryBucket
        {
            Name = request.Name,
            GroupType = request.GroupType,
            Year = request.Year
        };

        _dbContext.LibraryBuckets.Add(bucket);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateBucketResponse(bucket.LibraryBucketId, bucket.Name);
    }
}

// 5. Response
public record CreateBucketResponse(int Id, string Name);
```
