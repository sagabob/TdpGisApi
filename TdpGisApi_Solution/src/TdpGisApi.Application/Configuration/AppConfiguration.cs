using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.Configuration;

public class AppConfiguration
{
    public string Name { get; set; } = null!;
    public DbType DatabaseType { get; set; }
    public string ConnectionString { get; set; } = null!;
    public string Database { get; set; } = null!;
    public ICollection<string> Collections { get; set; } = null!;
}