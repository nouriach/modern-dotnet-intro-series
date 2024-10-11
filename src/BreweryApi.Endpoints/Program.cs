using BreweryApi.Application.Abstractions;
using BreweryApi.Domain.Models;
using BreweryApi.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Application = BreweryApi.Application.Extensions;
using Persistence = BreweryApi.Persistence.Extensions;

var builder = WebApplication.CreateBuilder();

builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

Application.DependencyInjection.RegisterServices(builder.Services);
Persistence.DependencyInjection.RegisterServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHealthChecks("/health");

app.MapGet("/brewery", async (IBreweryService breweryService) =>
{
    return await breweryService.GetAllBreweries();
});

app.MapGet("/brewery/{id}", async(IBreweryService breweryService, string id) => {
    Guid breweryId;
    var validId = Guid.TryParse(id, out breweryId);
    
    if (validId)
    {
        var matchingBrewery = await breweryService.GetBreweryById(breweryId);
        if (matchingBrewery == null)
            return Results.NotFound($"No brewery was found with the id: {id}");

        return Results.Ok(matchingBrewery);
    }

    return Results.BadRequest($"The Id you passed was not a Guid: {id}");
});

app.MapPost("/brewery", async(IBreweryService breweryService, BreweryUpsertRequest brewery) =>
{
    var createdBrewery = await breweryService.CreateBrewery(brewery);
    return Results.Ok(createdBrewery);
});

app.MapPut("/brewery/{id}", async(IBreweryService breweryService, string id, BreweryUpsertRequest updatedBrewery) =>
{
    Guid breweryId;
    var validId = Guid.TryParse(id, out breweryId);

    if (validId)
    {
        var matchingBrewery = await breweryService.UpdateBrewery(breweryId, updatedBrewery);
        if (matchingBrewery == null)
            return Results.NotFound($"No brewery was found with the id: {id}");

        return Results.Ok(matchingBrewery);
    }

    return Results.BadRequest($"The Id you passed was not a Guid: {id}");
});

app.MapDelete("/brewery/{id}", async(IBreweryService breweryService, string id) => {
    Guid breweryId;
    var validId = Guid.TryParse(id, out breweryId);

    if (validId)
    {
        var remainingBreweries = await breweryService.DeleteBrewery(breweryId);
        if (remainingBreweries == null)
            return Results.NotFound("Sorry, this brewery does not exist.");

        return Results.Ok(remainingBreweries);
    }

    return Results.BadRequest("The Id you passed was not a Guid.");
});

app.Run();