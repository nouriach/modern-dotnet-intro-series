using BreweryApi.Application.Abstractions;
using BreweryApi.Domain.Entities;
using BreweryApi.Domain.Models;
using BreweryApi.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BreweryApi.Persistence.Repositories;

public class BreweryRepository : IBreweryRepository
{
    private readonly DataContext _context;

    public BreweryRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Brewery>> GetAllBreweries()
    {
        return await _context.Breweries.ToListAsync();    
    }

    public async Task<Brewery?> GetBreweryById(Guid breweryId)
    {
        return await _context.Breweries.FirstOrDefaultAsync(b => b.Id == breweryId);
    }

    public async Task<Brewery> CreateBrewery(Brewery brewery)
    {
        await _context.Breweries.AddAsync(brewery);
        await _context.SaveChangesAsync();

        return brewery;
    }

    public async Task<Brewery?> UpdateBrewery(Guid id, Brewery brewery)
    {
        var breweryToUpdate = await GetBreweryById(id);
        if(breweryToUpdate == null)
            return null;

        breweryToUpdate.Name = brewery.Name ?? breweryToUpdate.Name;
        breweryToUpdate.City = brewery.City ?? breweryToUpdate.City;
        breweryToUpdate.State = brewery.State ?? breweryToUpdate.State;
        breweryToUpdate.WebsiteUrl = brewery.WebsiteUrl ?? breweryToUpdate.WebsiteUrl;

        await _context.SaveChangesAsync();

        return breweryToUpdate;
    }

    public async Task<IEnumerable<Brewery>?> DeleteBrewery(Guid id)
    {
        var breweryToRemove = await GetBreweryById(id);
        if (breweryToRemove == null)
            return null;

        _context.Breweries.Remove(breweryToRemove);

        await _context.SaveChangesAsync();

        return await _context.Breweries.ToListAsync();
    }
}