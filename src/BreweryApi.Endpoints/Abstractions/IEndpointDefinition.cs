namespace BreweryApi.Endpoints.Abstractions;

public interface IEndpointDefinition
{
    void RegisterEndpoints(IEndpointRouteBuilder routeBuilder);
}