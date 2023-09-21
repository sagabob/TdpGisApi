using Microsoft.EntityFrameworkCore;
using TdpGisApi.Application.Context;
using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.Handlers.Core;

public class LoadAppConfigurationHandler : ILoadAppConfigurationHandler
{
    private readonly IDbContextFactory<CosmosGisAppContext> _contextFactory;


    public LoadAppConfigurationHandler(IDbContextFactory<CosmosGisAppContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<ConnectionObject>> Connections()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var first = await context.AppConnections.AsNoTracking().ToListAsync();
        return first;
    }

    public async Task<List<QueryConfig>> Features()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var features = await context.AppFeatures.ToListAsync();
        foreach (var feature in features) await context.Entry(feature).Reference(x => x.Connection).LoadAsync();


        return features;
    }
}