using BreweryApi.Application.Abstractions;
using BreweryApi.Application.Features.Interfaces.Queries;
using BreweryApi.Domain.Models;

namespace BreweryApi.Application.Features.Breweries.Queries.GetBreweryById;

public class GetBreweryByIdHandler : IQueryHandler<GetBreweryById, BreweryResponse?>
{
    private IBreweryRepository _breweryRepository;

    public GetBreweryByIdHandler(IBreweryRepository breweryRepository)
    {
        _breweryRepository = breweryRepository;
    }

    public async Task<BreweryResponse?> Handle(GetBreweryById request, CancellationToken cancellationToken)
    {
        var brewery = await _breweryRepository.GetBreweryById(request.Id);
        if(brewery == null)
            return null;

        return new BreweryResponse(brewery);    
    }
}