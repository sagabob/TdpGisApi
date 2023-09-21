using TdpGisApi.Application.Extensions;
using TdpGisApi.Application.Handlers;
using TdpGisApi.Application.Handlers.Core;
using TdpGisApi.Endpoints.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.LoadApplicationConfiguration(builder.Configuration);
builder.Services.AddSingleton<ILoadAppConfigurationHandler, LoadAppConfigurationHandler>();

var app = builder.Build();

var conn = await app.Services.GetService<ILoadAppConfigurationHandler>()!.Features();

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