using BreweryApi.Application.Abstractions;
using BreweryApi.Application.Features.Interfaces.Commands;
using BreweryApi.Domain.Models;

namespace BreweryApi.Application.Features.Breweries.Commands.DeleteBrewery;

public class DeleteBreweryHandler : ICommandHandler<DeleteBrewery, IEnumerable<BreweryResponse>>
{
    private readonly IBreweryRepository _breweryRepository;

    public DeleteBreweryHandler(IBreweryRepository breweryRepository)
    {
        _breweryRepository = breweryRepository;
    }

    public async Task<IEnumerable<BreweryResponse>> Handle(DeleteBrewery request, CancellationToken cancellationToken)
    {
        var breweries = await _breweryRepository.DeleteBrewery(request.Id);
        return breweries.Select(brewery => new BreweryResponse(brewery));  
    }
}