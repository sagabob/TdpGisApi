using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TdpGisApi.Application.Context;
using TdpGisApi.Application.Models;

namespace SampleApp;

public class DbService
{
    private readonly IConfiguration _configuration;
    private readonly IDbContextFactory<CosmosGisAppContext> _contextFactory;
    private readonly CosmosClient _cosmosClient;

    public DbService(IDbContextFactory<CosmosGisAppContext> contextFactory, CosmosClient cosmosClient,
        IConfiguration configuration)
    {
        _contextFactory = contextFactory;
        _cosmosClient = cosmosClient;
        _configuration = configuration;
    }

    private async Task RecreateDatabase()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        SetIndex();
    }

    private void SetIndex()
    {
        _cosmosClient.GetDatabase("GisApp")
            .DefineContainer("AppConnections", "/DbType")
            .WithUniqueKey()
            .Path("/Name")
            .Attach();
    }

    public async Task RunSample()
    {
        await RecreateDatabase();

        await using var defaultContext = await _contextFactory.CreateDbContextAsync();

        await AddItemsFromDefaultContext(defaultContext);
    }

    private async Task AddItemsFromDefaultContext(CosmosGisAppContext defaultContext)
    {
        var conn = new ConnectionObject
        {
            Id = new Guid(),
            DatabaseId = "GeoDatabases",
            ConnectionString = _configuration["CosmosReadOnlyConnectionString"]!,
            ConnectionType = ConnectionType.ConnectionString,
            DbType = DbType.Cosmosdb,
            Name = "Main Connection",
            IsDisabled = false
        };

        var output1 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "ParkName",
            OutputName = "Name",
            PropertyType = PropertyType.Normal
        };
        var output2 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "ParkTypeDescription",
            OutputName = "Type",
            PropertyType = PropertyType.Normal
        };

        var output3 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "Area",
            OutputName = "Area",
            PropertyType = PropertyType.Normal
        };
        var output4 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "Location",
            OutputName = "Location",
            PropertyType = PropertyType.Spatial
        };

        var feature = new QueryConfig
        {
            Id = new Guid(),
            Name = "Parks",
            DisplayName = "Parks",
            CollectionName = "Parks",
            PartitionKey = "Locality",
            Description = "Parks in CC",
            Connection = conn,
            QueryType = QueryType.Text,
            QueryField = "ParkName",
            Mappings = new List<PropertyOutput> { output1, output2, output3, output4 },
            IsDisabled = false,
            ShowLevel = ShowLevel.Public,
            GeometryType = GeometryType.Polygon
        };

        defaultContext.Add(conn);
        defaultContext.Add(feature);
        await defaultContext.SaveChangesAsync();
    }
}