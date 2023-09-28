using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using TdpGisApi.Application.Configuration;
using TdpGisApi.Application.Extensions;
using TdpGisApi.Application.Factory;
using TdpGisApi.Application.Models;
using TdpGisApi.Endpoints.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var appSettings = new AppConfiguration();
builder.Configuration.GetSection(nameof(AppConfiguration)).Bind(appSettings);
builder.Services.AddSingleton(appSettings);
builder.Services.AddCors();

if (appSettings.DatabaseType == DbType.Cosmosdb) builder.Services.AddScoped<IGisAppFactory, CosmosGisAppFactory>();

builder.Services.RegisterAutoMapper();
builder.Services.RegisterHandlers();
builder.Services.RegisterComosComponents();

var app = builder.Build();

// global error handler
app.UseMiddleware<CustomExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();

app.Run();