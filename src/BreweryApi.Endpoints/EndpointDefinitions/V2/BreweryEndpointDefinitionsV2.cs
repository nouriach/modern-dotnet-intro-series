using BreweryApi.Application.Abstractions;
using BreweryApi.Endpoints.Abstractions;

namespace BreweryApi.Endpoints.EndpointDefinitions.V2;

public class BreweryEndpointDefinitionsV2 : IEndpointDefinition
{
    public void RegisterEndpoints(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/brewery", GetBreweries).MapToApiVersion(2);
        routeBuilder.MapGet("/brewery/{id}", GetBreweryById).MapToApiVersion(2);
    }

    private async Task<IResult> GetBreweries(IApiClientService breweryService)
    {
        var breweries = await breweryService.GetBreweries();
        return Results.Ok(breweries);
    }

    private async Task<IResult> GetBreweryById(IApiClientService breweryService, Guid id)
    {
        var brewery = await breweryService.GetBreweryById(id);
        return brewery is null ? Results.NotFound() : Results.Ok(brewery);
    }
}