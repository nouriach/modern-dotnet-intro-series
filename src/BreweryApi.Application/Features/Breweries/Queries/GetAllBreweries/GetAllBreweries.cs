using BreweryApi.Application.Features.Interfaces.Queries;
using BreweryApi.Domain.Models;

namespace BreweryApi.Application.Features.Breweries.Queries.GetAllBreweries;

public class GetAllBreweries : IQuery<IEnumerable<BreweryResponse>> { }