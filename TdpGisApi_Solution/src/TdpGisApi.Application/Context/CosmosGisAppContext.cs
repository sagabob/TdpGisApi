using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.Context;

public class CosmosGisAppContext : GisAppContext
{
    public CosmosGisAppContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConnectionObject>()
            .HasNoDiscriminator()
            .HasPartitionKey(x => x.DbType)
            .ToContainer("AppConnections")
            .HasKey(conn => conn.Id);

        modelBuilder.Entity<ConnectionObject>().Property(d => d.ConnectionType)
            .HasConversion(new EnumToStringConverter<ConnectionType>());

        modelBuilder.Entity<ConnectionObject>().Property(d => d.DbType)
            .HasConversion(new EnumToStringConverter<DbType>());

        //enforce the readable field Name to be unique
        modelBuilder.Entity<ConnectionObject>().HasIndex(b => b.Name).IsUnique();


        modelBuilder.Entity<QueryConfig>()
            .HasNoDiscriminator()
            .HasPartitionKey(x => x.ShowLevel)
            .ToContainer("AppFeatures")
            .HasKey(conn => conn.Id);

        modelBuilder.Entity<QueryConfig>().Property(x => x.GeometryType)
            .HasConversion(new EnumToStringConverter<GeometryType>());

        modelBuilder.Entity<QueryConfig>().Property(x => x.QueryType)
            .HasConversion(new EnumToStringConverter<QueryType>());

        modelBuilder.Entity<QueryConfig>().Property(x => x.ShowLevel)
            .HasConversion(new EnumToStringConverter<ShowLevel>());

        modelBuilder.Entity<QueryConfig>().OwnsMany(p => p.Mappings,
            a => { a.Property(x => x.PropertyType).HasConversion(new EnumToStringConverter<PropertyType>()); });
    }
}