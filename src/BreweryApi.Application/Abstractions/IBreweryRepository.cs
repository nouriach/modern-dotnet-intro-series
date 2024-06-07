using BreweryApi.Domain.Entities;

namespace BreweryApi.Application.Abstractions;

public interface IBreweryRepository
{
    Task<IEnumerable<Brewery>> GetAllBreweries();
    Task<Brewery?> GetBreweryById(Guid id);
    Task<Brewery> CreateBrewery(Brewery brewery);
    Task<Brewery?> UpdateBrewery(Guid id, Brewery brewery);
    Task<IEnumerable<Brewery>?> DeleteBrewery(Guid id);
}