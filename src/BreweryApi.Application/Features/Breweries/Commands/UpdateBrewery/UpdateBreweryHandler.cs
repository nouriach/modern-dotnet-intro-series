using BreweryApi.Application.Abstractions;
using BreweryApi.Application.Features.Interfaces.Commands;
using BreweryApi.Domain.Entities;
using BreweryApi.Domain.Models;

namespace BreweryApi.Application.Features.Breweries.Commands.UpdateBrewery;

public class UpdateBreweryHandler : ICommandHandler<UpdateBreweryRequest, BreweryResponse>
{
    private readonly IBreweryRepository _breweryRepository;

    public UpdateBreweryHandler(IBreweryRepository breweryRepository)
    {
        _breweryRepository = breweryRepository;
    }

    public async Task<BreweryResponse> Handle(UpdateBreweryRequest request, CancellationToken cancellationToken)
    {
        var breweryEntity = new Brewery
        {
            Name = request.Command.Name,
            City = request.Command.City,
            State = request.Command.State,
            WebsiteUrl = request.Command.WebsiteUrl
        };

        var breweryToReturn = await _breweryRepository.UpdateBrewery(request.Id, breweryEntity);
        if(breweryToReturn == null)
            return null;

        return new BreweryResponse(breweryToReturn);    
    }
}