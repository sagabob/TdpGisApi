namespace TdpGisApi.Core.Models;

public class ConnectionObject
{
    public Guid Id { get; set; }

    public required string ConnectionString { get; set; }

    public required string DatabaseId { get; set; }
    public required List<string> Collections { get; set; }

    public ConnectionType ConnectionType { get; set; }
}

public enum ConnectionType
{
    AzureRbac,
    ConnectionString
}