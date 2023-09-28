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

        IDictionary<string, string> parameters = new Dictionary<string, string>
        {
            { "URL", _configuration["URL"]! },
            { "KEY", _configuration["KEY"]! }
        };
        var conn = new ConnectionObject
        {
            Id = new Guid(),
            DatabaseId = "GeoDatabases",
            ConnectionParameters = parameters,
            ConnectionType = ConnectionType.ConnectionString,
            DbType = DbType.Cosmosdb,
            Name = "Main Connection",
            IsDisabled = false
        };

        var park1 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "ParkName",
            OutputName = "Title",
            PropertyType = PropertyType.Normal
        };
        var park2 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "ParkTypeDescription",
            OutputName = "Type",
            PropertyType = PropertyType.Normal
        };

        var park3 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "Area",
            OutputName = "Area",
            PropertyType = PropertyType.Normal
        };
        var park4 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "Location",
            OutputName = "Location",
            PropertyType = PropertyType.Spatial
        };

        var parkFeature = new QueryConfig
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
            Mappings = new List<PropertyOutput> { park1, park2, park3, park4 },
            IsDisabled = false,
            ShowLevel = ShowLevel.Public,
            GeometryType = GeometryType.Polygon
        };

        var sta1 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "StreetAddress",
            OutputName = "Title",
            PropertyType = PropertyType.Normal
        };
        var sta2 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "StreetAddressStatusDescription",
            OutputName = "Current",
            PropertyType = PropertyType.Normal
        };

        var sta3 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "OccupationLevelDescription",
            OutputName = "OccupationLevel",
            PropertyType = PropertyType.Normal
        };

        var sta4 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "Locality",
            OutputName = "Locality",
            PropertyType = PropertyType.Normal
        };

        var sta5 = new PropertyOutput
        {
            Id = new Guid(),
            PropertyName = "Location",
            OutputName = "Location",
            PropertyType = PropertyType.Spatial
        };

        var streetAddressFeature = new QueryConfig
        {
            Id = new Guid(),
            Name = "StreetAddresses",
            DisplayName = "Street Address",
            CollectionName = "StreetAddresses",
            PartitionKey = "Locality",
            Description = "Street Addresses in CC",
            Connection = conn,
            QueryType = QueryType.Text,
            QueryField = "StreetAddress",
            Mappings = new List<PropertyOutput> { sta1, sta2, sta3, sta4, sta5 },
            IsDisabled = false,
            ShowLevel = ShowLevel.Public,
            GeometryType = GeometryType.Point
        };

        defaultContext.Add(conn);
        defaultContext.Add(parkFeature);
        defaultContext.Add(streetAddressFeature);
        await defaultContext.SaveChangesAsync();
    }
}