namespace TdpGisApi.Application.Models.Core;

public class ConnectionObject
{
    public required string Name { get; set; }

    public required IDictionary<string, string> ConnectionParameters { get; set; }

    public required string DatabaseId { get; set; }

    public required ConnectionType ConnectionType { get; set; } = ConnectionType.ConnectionString;

    public required DbType DbType { get; set; }

    public Guid Id { get; set; }

    public required bool IsDisabled { get; set; }
}