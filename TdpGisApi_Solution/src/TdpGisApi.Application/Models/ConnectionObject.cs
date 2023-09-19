namespace TdpGisApi.Application.Models;

public class ConnectionObject
{
    public required string Name { get; set; }

    public required string ConnectionString { get; set; }

    public required string DatabaseId { get; set; }

    public ConnectionType ConnectionType { get; set; }

    public DbType DbType { get; set; }

    public Guid Id { get; set; }
}