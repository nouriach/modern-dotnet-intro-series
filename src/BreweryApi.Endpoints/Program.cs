var builder = WebApplication.CreateBuilder();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseHealthChecks("/health");

app.Run();