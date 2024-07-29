using BreweryApi.Endpoints.Abstractions;

namespace BreweryApi.Endpoints.Extensions;

public static class DependencyInjection
{
    public static void RegisterEndpointDefinitions(this IEndpointRouteBuilder routeBuilder)
    {
        var endpointDefinitions = typeof(Program).Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition))
                        && t is { IsAbstract: false, IsInterface: false })
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();

        foreach(var endpointDef in endpointDefinitions)
            endpointDef.RegisterEndpoints(routeBuilder);
    }
}