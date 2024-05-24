using BreweryApi.Domain.Models;

namespace BreweryApi.Application.Abstractions;

public interface IBreweryRepository
{
    IEnumerable<Brewery> GetAllBreweries();
    Brewery GetBreweryById(Guid id);
    Brewery CreateBrewery(Brewery brewery);
    Brewery UpdateBrewery(Guid id, Brewery brewery);
    IEnumerable<Brewery> DeleteBrewery(Guid id);
}