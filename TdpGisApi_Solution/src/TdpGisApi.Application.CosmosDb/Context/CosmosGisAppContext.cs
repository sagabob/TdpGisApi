using Microsoft.EntityFrameworkCore;
using TdpGisApi.Application.Context;
using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.CosmosDb.Context;

public class CosmosGisAppContext : GisAppContext
{
    public CosmosGisAppContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConnectionObject>()
            .HasNoDiscriminator()
            .ToContainer("AppConnections")
            .HasKey(conn => conn.Id);

        modelBuilder.Entity<QueryConfig>()
            .HasNoDiscriminator()
            .ToContainer("AppFeatures")
        
            .HasKey(conn => conn.Id);
    }
}