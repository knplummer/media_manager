using MediaManager.Infrastructure.Persistence;
using MediaManager.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using FluentValidation;
using MediaManager.Infrastructure.Endpoints;
using MediaManager.Features.UserManagement.Abstractions.Interfaces;
using MediaManager.Features.UserManagement.Repositories;
using MediaManager.Features.UserManagement.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AuditableEntitySaveChangesInterceptor>();
builder.Services.AddScoped<IUserManagementRepository, UserManagementRepository>();
builder.Services.AddScoped<IUserManagementValidationRepository, UserManagementValidationRepository>();
builder.Services.AddSingleton<UserManagementMapper>();

builder.Services.AddDbContext<MediaManagerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMediator(x =>
{
    x.AddConsumers(typeof(Program).Assembly);
});

builder.Services.AddMassTransit(x => 
{
    x.AddConsumers(typeof(Program).Assembly);

    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Apply pending migrations on startup in Development
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<MediaManagerDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapEndpoints(typeof(Program).Assembly);

app.Run();
