using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ProductionFacilities.Application.Extensions;
using ProductionFacilities.Infrastructure.Extensions;
using ProductionFacilities.Infrastructure.Persistence;
using ProductionFacilities.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAplication();

builder.Services.AddControllers();

var app = builder.Build();

using (var migrateScope = app.Services.CreateScope())
{
    var dbContext = migrateScope.ServiceProvider.GetRequiredService<ProductionFacilitiesDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseMiddleware<ProductionFacilities.WebApi.Middleware.AuthorizationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
