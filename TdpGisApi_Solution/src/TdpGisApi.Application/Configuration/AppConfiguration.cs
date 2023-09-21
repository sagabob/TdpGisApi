namespace TdpGisApi.Endpoints.Models;

public class AppConfiguration
{
    public string Name { get; set; } = null!;
    public string DatabaseType { get; set; } = null!;
    public string ConnectionString { get; set; } = null!;
    public string Database { get; set; } = null!;
    public ICollection<string> Collections { get; set; } = null!;
}