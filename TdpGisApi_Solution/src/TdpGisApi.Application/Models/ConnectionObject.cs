namespace TdpGisApi.Application.Models;

public class ConnectionObject
{
    public required string Name { get; set; }

    public required IDictionary<string, string> ConnectionParameters { get; set; }

    public required string DatabaseId { get; set; }

    public required ConnectionType ConnectionType { get; set; }

    public required DbType DbType { get; set; }

    public Guid Id { get; set; }

    public required bool IsDisabled { get; set; }
}