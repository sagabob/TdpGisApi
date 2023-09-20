using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        modelBuilder.Entity<ConnectionObject>().Property(d => d.ConnectionType)
            .HasConversion(new EnumToStringConverter<ConnectionType>());

        modelBuilder.Entity<ConnectionObject>().Property(d => d.DbType)
            .HasConversion(new EnumToStringConverter<DbType>());

        modelBuilder.Entity<QueryConfig>()
            .HasNoDiscriminator()
            .ToContainer("AppFeatures")
            .HasKey(conn => conn.Id);

        modelBuilder.Entity<QueryConfig>().Property(x => x.GeometryType)
            .HasConversion(new EnumToStringConverter<GeometryType>());

        modelBuilder.Entity<QueryConfig>().Property(x => x.QueryType)
            .HasConversion(new EnumToStringConverter<QueryType>());

        modelBuilder.Entity<QueryConfig>().OwnsMany(p => p.Mappings,
            a => { a.Property(x => x.PropertyType).HasConversion(new EnumToStringConverter<PropertyType>()); });
    }
}