using System.ComponentModel.DataAnnotations;
using TdpGisApi.Core;

namespace TdpGisApi.Application.Models.Core;

public class ConnectionObject : IEntity
{
    [Key]
    public Guid Id { get; set; }

    public required string ConnectionString { get; set; }

    public required string DatabaseId { get; set; }
    public required List<string> Collections { get; set; }

    public ConnectionType ConnectionType { get; set; }
}