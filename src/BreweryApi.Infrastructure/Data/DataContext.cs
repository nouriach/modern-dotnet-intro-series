using BreweryApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BreweryApi.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public DbSet<Brewery> Breweries { get; set; }
}