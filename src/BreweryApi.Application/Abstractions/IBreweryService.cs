using BreweryApi.Domain.Models;

namespace BreweryApi.Application.Abstractions;

public interface IBreweryService
{
    Task<IEnumerable<BreweryResponse>> GetAllBreweries();
    Task<BreweryResponse?> GetBreweryById(Guid id);
    Task<BreweryResponse> CreateBrewery(BreweryUpsertRequest brewery);
    Task<BreweryResponse?> UpdateBrewery(Guid id, BreweryUpsertRequest brewery);
    Task<IEnumerable<BreweryResponse>?> DeleteBrewery(Guid id);
}