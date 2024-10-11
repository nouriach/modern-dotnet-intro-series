using BreweryApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BreweryApi.Persistence.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public DbSet<Brewery> Breweries { get; set; }
}