using BreweryApi.Application.Abstractions;
using BreweryApi.Application.Features.Interfaces.Commands;
using BreweryApi.Domain.Models;

namespace BreweryApi.Application.Features.Breweries.Queries.GetAllBreweries;

public class GetAllBreweriesHandler : ICommandHandler<GetAllBreweries, IEnumerable<BreweryResponse>>
{
    private IBreweryRepository _breweryRepository;

    public GetAllBreweriesHandler(IBreweryRepository breweryRepository)
    {
        _breweryRepository = breweryRepository;
    }

    public async Task<IEnumerable<BreweryResponse>> Handle(GetAllBreweries request, CancellationToken cancellationToken)
    {
        var breweries = await _breweryRepository.GetAllBreweries();
        return breweries.Select(brewery => new BreweryResponse(brewery));
    }
}