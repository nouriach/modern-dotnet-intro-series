using Asp.Versioning;
using Asp.Versioning.Builder;
using BreweryApi.Endpoints.Extensions;
using BreweryApi.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Application = BreweryApi.Application.Extensions;
using Persistence = BreweryApi.Persistence.Extensions;
using Infrastructure = BreweryApi.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder();

builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API V1", Version = "v1" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "API V2", Version = "v2" });
});

builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

Application.DependencyInjection.RegisterServices(builder.Services);
Persistence.DependencyInjection.RegisterServices(builder.Services);
Infrastructure.DependencyInjection.RegisterServices(builder.Services, builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
    });
}
app.UseHealthChecks("/health");

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .HasApiVersion(new ApiVersion(2))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app.MapGroup("api/v{apiVersion:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

versionedGroup.RegisterEndpointDefinitions();

app.Run();