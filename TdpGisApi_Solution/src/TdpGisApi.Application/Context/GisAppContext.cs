using Microsoft.EntityFrameworkCore;
using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.Context;

public class GisAppContext : DbContext
{
    public GisAppContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<ConnectionObject> AppConnections { get; set; } = null!;

    public DbSet<QueryConfig> AppFeatures { get; set; } = null!;

    public DbSet<FeatureLayer> AppLayers { get; set; } = null!;
}