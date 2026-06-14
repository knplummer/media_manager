using MediaManager.Infrastructure.Persistence;
using MediaManager.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using FluentValidation;
using MediaManager.Infrastructure.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<AuditableEntitySaveChangesInterceptor>();
builder.Services.AddDbContext<MediaManagerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMediator(cfg => 
{
    cfg.AddConsumers(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapEndpoints(typeof(Program).Assembly);

app.Run();
