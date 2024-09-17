using BreweryApi.Application.Abstractions;
using BreweryApi.Domain.Models;
using Application = BreweryApi.Application.Extensions;
using Infrastructure = BreweryApi.Persistence.Extensions;

var builder = WebApplication.CreateBuilder();

builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Application.DependencyInjection.RegisterServices(builder.Services);
Infrastructure.DependencyInjection.RegisterServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHealthChecks("/health");

app.MapGet("/brewery", (IBreweryService breweryService) =>
{
    return breweryService.GetAllBreweries();
});

app.MapGet("/brewery/{id}", (IBreweryService breweryService, string id) => {
    Guid breweryId;
    var validId = Guid.TryParse(id, out breweryId);
    
    if (validId)
    {
        var matchingBrewery = breweryService.GetBreweryById(breweryId);
        if (matchingBrewery == null)
            return Results.NotFound($"No brewery was found with the id: {id}");

        return Results.Ok(matchingBrewery);
    }

    return Results.BadRequest($"The Id you passed was not a Guid: {id}");
});

app.MapPost("/brewery", (IBreweryService breweryService, Brewery brewery) =>
{
    var createdBrewery = breweryService.CreateBrewery(brewery);
    return Results.Ok(createdBrewery);
});

app.MapPut("/brewery/{id}", (IBreweryService breweryService, string id, Brewery updatedBrewery) =>
{
    Guid breweryId;
    var validId = Guid.TryParse(id, out breweryId);

    if (validId)
    {
        var matchingBrewery = breweryService.UpdateBrewery(breweryId, updatedBrewery);
        if (matchingBrewery == null)
            return Results.NotFound($"No brewery was found with the id: {id}");

        matchingBrewery.Name = updatedBrewery.Name;
        matchingBrewery.City = updatedBrewery.City;
        matchingBrewery.State = updatedBrewery.State;
        matchingBrewery.WebsiteUrl = updatedBrewery.WebsiteUrl;

        return Results.Ok(matchingBrewery);
    }

    return Results.BadRequest($"The Id you passed was not a Guid: {id}");
});

app.MapDelete("/brewery/{id}", (IBreweryService breweryService, string id) => {
    Guid breweryId;
    var validId = Guid.TryParse(id, out breweryId);

    if (validId)
    {
        var remainingBreweries = breweryService.DeleteBrewery(breweryId);
        if (remainingBreweries == null)
            return Results.NotFound("Sorry, this brewery does not exist.");

        return Results.Ok(remainingBreweries);
    }

    return Results.BadRequest("The Id you passed was not a Guid.");
});

app.Run();