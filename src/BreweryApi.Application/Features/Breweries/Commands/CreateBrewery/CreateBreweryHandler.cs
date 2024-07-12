using BreweryApi.Application.Abstractions;
using BreweryApi.Application.Features.Interfaces.Commands;
using BreweryApi.Domain.Entities;
using BreweryApi.Domain.Models;

namespace BreweryApi.Application.Features.Breweries.Commands.CreateBrewery;

public class CreateBreweryHandler : ICommandHandler<CreateBrewery, BreweryResponse>
{
    private readonly IBreweryRepository _breweryRepository;

    public CreateBreweryHandler(IBreweryRepository breweryRepository)
    {
        _breweryRepository = breweryRepository;
    }

    public async Task<BreweryResponse> Handle(CreateBrewery brewery, CancellationToken cancellationToken)
    {
        var breweryEntity = new Brewery
        {
            Name = brewery.Name,
            City = brewery.City,
            State = brewery.State,
            WebsiteUrl = brewery.WebsiteUrl
        };

        var breweryToReturn = await _breweryRepository.CreateBrewery(breweryEntity);

        return new BreweryResponse(breweryToReturn);
    }    
}