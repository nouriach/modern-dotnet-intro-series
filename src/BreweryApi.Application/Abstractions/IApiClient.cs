using BreweryApi.Domain.Entities;

namespace BreweryApi.Application.Abstractions;

public interface IApiClient
{
    Task<Brewery?> GetBreweryById(Guid id);
    Task<IEnumerable<Brewery>?> GetBreweries();
}