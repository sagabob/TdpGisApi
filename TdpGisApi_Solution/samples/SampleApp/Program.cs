using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleApp;
using TdpGisApi.Application.Context;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var cosmosConnectionString = config["CosmosConnectionString"];

var services = new ServiceCollection();

services.AddDbContextFactory<CosmosGisAppContext>(optionsBuilder =>
    optionsBuilder
        .UseCosmos(
            cosmosConnectionString!,
            "GisApp",
            options =>
            {
                options.ConnectionMode(ConnectionMode.Direct);
                options.MaxRequestsPerTcpConnection(20);
                options.MaxTcpConnectionsPerEndpoint(32);
            }));

services.AddSingleton(new CosmosClient(cosmosConnectionString));

services.AddSingleton<IConfiguration>(config);

services.AddTransient<DbService>();

await using var serviceProvider = services.BuildServiceProvider();

var transportService = serviceProvider.GetRequiredService<DbService>();

await transportService.RunSample();

Console.WriteLine();
Console.WriteLine("Done. Press ENTER to quit.");
Console.ReadLine();