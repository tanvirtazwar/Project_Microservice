using Catalog.API.DatabaseSettings;
using Catalog.API.DatabaseSettings.Interfaces;
using Catalog.API.Repositories;
using Catalog.API.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<CatalogDatabaseSettings>(
    builder.Configuration.GetSection("CatalogDatabase"));

builder.Services.AddSingleton<ICatalogDatabaseSettings>(
    sp => sp.GetRequiredService<IOptions<CatalogDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(
    sp => new MongoClient(
        builder.Configuration.GetValue<string>("CatalogDatabase:ConnectionString")));

builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
