using BreweryApi.Application.Features.Breweries.Commands.CreateBrewery;
using BreweryApi.Application.Features.Breweries.Commands.DeleteBrewery;
using BreweryApi.Application.Features.Breweries.Commands.UpdateBrewery;
using BreweryApi.Application.Features.Breweries.Queries.GetAllBreweries;
using BreweryApi.Application.Features.Breweries.Queries.GetBreweryById;
using BreweryApi.Endpoints.Abstractions;
using MediatR;

namespace BreweryApi.Endpoints.EndpointDefinitions.V1;

public class BreweryEndpointDefinitions : IEndpointDefinition
{
    public void RegisterEndpoints(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/brewery", CreateBrewery).MapToApiVersion(1);
        routeBuilder.MapDelete("/brewery/{id}", DeleteBrewery).MapToApiVersion(1);
        routeBuilder.MapGet("/brewery", GetBreweries).MapToApiVersion(1);
        routeBuilder.MapGet("/brewery/{id}", GetBreweryById).MapToApiVersion(1);
        routeBuilder.MapPut("/brewery/{id}", UpdateBrewery).MapToApiVersion(1);
    }

    private async Task<IResult> CreateBrewery(IMediator mediator, CreateBrewery command)
    {
        var createdBrewery = await mediator.Send(command);
        return Results.Ok(createdBrewery);
    }

    private async Task<IResult> DeleteBrewery(IMediator mediator, string id)
    {
        Guid breweryId;
        var validId = Guid.TryParse(id, out breweryId);

        if (validId)
        {
            var remainingBreweries = await mediator.Send(new DeleteBrewery { Id = breweryId });
            return Results.Ok(remainingBreweries);
        }

        return Results.BadRequest("The Id you passed was not a Guid.");
    }

    private async Task<IResult> GetBreweries(IMediator mediator)
    {
        var breweries = await mediator.Send(new GetAllBreweries());
        return Results.Ok(breweries);
    }

    private async Task<IResult> GetBreweryById(IMediator mediator, string id)
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
    }

    private async Task<IResult> UpdateBrewery(IMediator mediator, string id, UpdateBrewery command)
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
    }
}