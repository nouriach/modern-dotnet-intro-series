var builder = WebApplication.CreateBuilder();

builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHealthChecks("/health");

var breweries = new List<Brewery>
{
    new Brewery { Id = Guid.Parse("5128df48-79fc-4f0f-8b52-d06be54d0cec"), Name = "(405) Brewing Co", City = "Norman", State = "Oklahoma", WebsiteUrl = "http://www.405brewing.com" },
    new Brewery { Id = Guid.Parse("9c5a66c8-cc13-416f-a5d9-0a769c87d318"), Name = "(512) Brewing Co", City = "Austin", State = "Texas", WebsiteUrl = "http://www.512brewing.com" },
    new Brewery { Id = Guid.Parse("34e8c68b-6146-453f-a4b9-1f6cd99a5ada"), Name = "1 of Us Brewing Company", City = "Mount Pleasant", State = "Wisconsin", WebsiteUrl = "https://www.1ofusbrewing.com" },
    new Brewery { Id = Guid.Parse("08f78223-24f8-4b71-b381-ea19a5bd82df"), Name = "11 Below Brewing Company", City = "Houston", State = "Texas", WebsiteUrl = "http://www.11belowbrewing.com" },
};

app.MapGet("/brewery", () => Results.Ok(breweries));

app.MapGet("/brewery/{id}", (string id) => {
    Guid breweryId;
    var validId = Guid.TryParse(id, out breweryId);
    
    if (validId)
    {
        var matchingBrewery = breweries.Find(brewery => brewery.Id == breweryId);
        if (matchingBrewery == null)
            return Results.NotFound($"No brewery was found with the id: {id}");

        return Results.Ok(matchingBrewery);
    }

    return Results.BadRequest($"The Id you passed was not a Guid: {id}");
});

app.MapPost("/brewery", (Brewery brewery) =>
{
    brewery.Id = Guid.NewGuid();
    breweries.Add(brewery);

    return Results.Ok(breweries);
});

app.MapPut("/brewery/{id}", (string id, Brewery updatedBrewery) =>
{
    Guid breweryId;
    var validId = Guid.TryParse(id, out breweryId);

    if (validId)
    {
        var matchingBrewery = breweries.Find(brewery => brewery.Id == breweryId);
        if (matchingBrewery == null)
            return Results.NotFound($"No brewery was found with the id: {id}");

        matchingBrewery.Name = updatedBrewery.Name;
        matchingBrewery.City = updatedBrewery.City;
        matchingBrewery.State = updatedBrewery.State;
        matchingBrewery.WebsiteUrl = updatedBrewery.WebsiteUrl;

        return Results.Ok(matchingBrewery);
    }

    return Results.BadRequest($"The Id you passed was not a Guid: {id}");
});

app.MapDelete("/brewery/{id}", (string id) => {
    Guid breweryId;
    var validId = Guid.TryParse(id, out breweryId);

    if (validId)
    {
        var matchingBrewery = breweries.Find(brewery => brewery.Id == breweryId);
        if (matchingBrewery == null)
            return Results.NotFound($"No brewery was found with the id: {id}");

        breweries.Remove(matchingBrewery);
        return Results.Ok(breweries);
    }

    return Results.BadRequest($"The Id you passed was not a Guid: {id}");
});

app.Run();

class Brewery
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string WebsiteUrl { get; set; }
}