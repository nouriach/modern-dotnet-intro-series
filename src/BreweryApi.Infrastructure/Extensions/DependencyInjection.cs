using BreweryApi.Application.Abstractions;
using BreweryApi.Infrastructure.Clients;
using BreweryApi.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BreweryApi.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BreweryApiConfiguration>(
            configuration.GetSection(BreweryApiConfiguration.ConfigSectionName)
        );

        services.AddHttpClient<IApiClient, BreweryApiClient>();

        return services;
    }
}