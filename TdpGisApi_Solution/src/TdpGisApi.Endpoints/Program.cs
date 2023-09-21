using TdpGisApi.Application.Configuration;
using TdpGisApi.Application.Extensions;
using TdpGisApi.Application.Factory;
using TdpGisApi.Application.Models;
using TdpGisApi.Endpoints.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var appSettings = new AppConfiguration();
builder.Configuration.GetSection(nameof(AppConfiguration)).Bind(appSettings);
builder.Services.AddSingleton(appSettings);


if (appSettings.DatabaseType == DbType.Cosmosdb) builder.Services.AddScoped<IGisAppFactory, CosmosGisAppFactory>();

builder.Services.RegisterAutoMapper();

var app = builder.Build();


app.UseMiddleware<CustomExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();