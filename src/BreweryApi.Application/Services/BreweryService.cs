using BreweryApi.Application.Abstractions;
using BreweryApi.Domain.Models;

namespace BreweryApi.Application.Services;

public class BreweryService : IBreweryService
{
    private IBreweryRepository _breweryRepository;
    public BreweryService(IBreweryRepository breweryRepository)
    {
        _breweryRepository = breweryRepository;
    }
    public IEnumerable<Brewery> GetAllBreweries()
    {
        return _breweryRepository.GetAllBreweries();
    }

    public Brewery  GetBreweryById(Guid breweryId)
    {
        return _breweryRepository.GetBreweryById(breweryId);
    }

    public Brewery  CreateBrewery(Brewery brewery)
    {
        return _breweryRepository.CreateBrewery(brewery);
    }

    public Brewery UpdateBrewery(Guid id, Brewery brewery)
    {
        return _breweryRepository.UpdateBrewery(id, brewery);
    }

    public IEnumerable<Brewery> DeleteBrewery(Guid id)
    {
        return _breweryRepository.DeleteBrewery(id);
    }
}