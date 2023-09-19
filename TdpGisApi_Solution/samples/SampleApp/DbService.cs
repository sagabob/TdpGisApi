using Microsoft.EntityFrameworkCore;
using TdpGisApi.Application.CosmosDb.Context;
using TdpGisApi.Application.Models;

namespace SampleApp;

public class DbService
{
    private readonly IDbContextFactory<CosmosGisAppContext> _contextFactory;

    public DbService(IDbContextFactory<CosmosGisAppContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    private async Task RecreateDatabase()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
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
            Name = "Main Connection"
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
            Mappings = new List<PropertyOutput>() { output1 }
        };

        defaultContext.Add(conn);
        defaultContext.Add(feature);
        await defaultContext.SaveChangesAsync();
    }
}