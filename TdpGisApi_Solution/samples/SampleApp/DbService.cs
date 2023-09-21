using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using TdpGisApi.Application.CosmosDb.Context;
using TdpGisApi.Application.Models;

namespace SampleApp;

public class DbService
{
    private readonly IDbContextFactory<CosmosGisAppContext> _contextFactory;
    private readonly CosmosClient _cosmosClient;

    public DbService(IDbContextFactory<CosmosGisAppContext> contextFactory, CosmosClient cosmosClient)
    {
        _contextFactory = contextFactory;
        _cosmosClient = cosmosClient;
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
            .DefineContainer(name: "AppConnections", partitionKeyPath: "/DbType")
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
        var conn = new ConnectionObject()
        {
            Id = new Guid(),
            DatabaseId = "GeoDatabases",
            ConnectionString = "Test",
            ConnectionType = ConnectionType.ConnectionString,
            DbType = DbType.Cosmosdb,
            Name = "Main Connection",
            IsDisabled = false
        };

        var output1 = new PropertyOutput()
        {
            Id = new Guid(),
            PropertyName = "Title",
            OutputName = "Description",
            PropertyType = PropertyType.Normal
        };

        var feature = new QueryConfig()
        {
            Id = new Guid(),
            Name = "Sample Feature",
            Description = "Sample",
            Connection = conn,
            QueryType = QueryType.Text,
            QueryField = "Name",
            Mappings = new List<PropertyOutput>() { output1 },
            IsDisabled = false,
            ShowLevel = ShowLevel.Public
        };

        defaultContext.Add(conn);
        defaultContext.Add(feature);
        await defaultContext.SaveChangesAsync();
    }
}