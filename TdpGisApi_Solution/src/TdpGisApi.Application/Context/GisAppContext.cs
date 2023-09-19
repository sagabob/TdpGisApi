using Microsoft.EntityFrameworkCore;
using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.Context;

public class GisAppContext : DbContext
{
    public GisAppContext(DbContextOptions options)
        : base(options)
    { }
    public DbSet<ConnectionObject> Connections { get; set; } = null!;

    public DbSet<QueryConfig> QueryConfigs { get; set; } = null!;
    
}