using BreweryApi.Application.Features.Breweries.Commands.CreateBrewery;
using BreweryApi.Application.Features.Breweries.Commands.DeleteBrewery;
using BreweryApi.Application.Features.Breweries.Commands.UpdateBrewery;
using BreweryApi.Application.Features.Breweries.Queries.GetAllBreweries;
using BreweryApi.Application.Features.Breweries.Queries.GetBreweryById;
using BreweryApi.Persistence.Data;
using MediatR;
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

app.MapGet("/brewery", async (IMediator mediator) =>
{
    return await mediator.Send(new GetAllBreweries());
});

app.MapGet("/brewery/{id}", async(IMediator mediator, string id) => 
{
    Guid breweryId;
    var validId = Guid.TryParse(id, out breweryId);
    
    if (validId)
    {
        var matchingBrewery = await mediator.Send(new GetBreweryById{ Id = breweryId });
        if (matchingBrewery == null)
            return Results.NotFound($"No brewery was found with the id: {id}");

        return Results.Ok(matchingBrewery);
    }

    return Results.BadRequest($"The Id you passed was not a Guid: {id}");
});

app.MapPost("/brewery", async(IMediator mediator, CreateBrewery command) =>
{
    var createdBrewery = await mediator.Send(command);
    return Results.Ok(createdBrewery);
});

app.MapPut("/brewery/{id}", async(IMediator mediator, string id, UpdateBrewery command) =>
{
    Guid breweryId;
    var validId = Guid.TryParse(id, out breweryId);

    if (validId)
    {
        var matchingBrewery = await mediator.Send(new UpdateBreweryRequest()
        {
            Id = breweryId,
            Command = command
        });
        if (matchingBrewery == null)
            return Results.NotFound($"No brewery was found with the id: {id}");

        return Results.Ok(matchingBrewery);
    }

    return Results.BadRequest($"The Id you passed was not a Guid: {id}");
});

app.MapDelete("/brewery/{id}", async(IMediator mediator, string id) => 
{
    Guid breweryId;
    var validId = Guid.TryParse(id, out breweryId);

    if (validId)
    {
        var remainingBreweries = await mediator.Send(new DeleteBrewery { Id = breweryId });
        return Results.Ok(remainingBreweries);
    }

    return Results.BadRequest("The Id you passed was not a Guid.");
});

app.Run();