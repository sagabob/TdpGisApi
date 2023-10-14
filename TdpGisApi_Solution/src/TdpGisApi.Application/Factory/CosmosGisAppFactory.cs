using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using TdpGisApi.Application.Configuration;
using TdpGisApi.Application.Context;

namespace TdpGisApi.Application.Factory;

public class CosmosGisAppFactory : IDbContextFactory<CosmosGisAppContext>, IGisAppFactory
{
    private readonly AppConfiguration _appSettings;


    public CosmosGisAppFactory(AppConfiguration configuration)
    {
        _appSettings = configuration;
    }

    public CosmosGisAppContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<CosmosGisAppContext>();
        optionsBuilder.UseCosmos(
            _appSettings.ConnectionString,
            _appSettings.Database,
            options =>
            {
                options.ConnectionMode(ConnectionMode.Direct);
                options.MaxRequestsPerTcpConnection(20);
                options.MaxTcpConnectionsPerEndpoint(32);
            });

        return new CosmosGisAppContext(optionsBuilder.Options);
    }

    public async Task<AppFeatureData> CreateAppFeatureData()
    {
        await using var context = CreateDbContext();
        var connections = await context.AppConnections.AsNoTracking().ToListAsync();
        var features = await context.AppFeatures.ToListAsync();

        var layers = await context.AppLayers.ToListAsync();

        foreach (var feature in features) await context.Entry(feature).Reference(x => x.Connection).LoadAsync();

        foreach (var layer in layers) await context.Entry(layer).Reference(x => x.Connection).LoadAsync();

        return new AppFeatureData
        {
            Connections = connections,
            Features = features,
            Layers = layers
        };
    }
}