using System.ComponentModel.DataAnnotations;

namespace TdpGisApi.Core.Models;

public class QueryConfig: IEntity
{
    [Key] public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public QueryType QueryType { get; set; }

    public required string QueryField { get; set; }

    public required List<PropertyOutput> Mappings { get; set; }

    public Guid ConnectionId { get; set; }

    public GeometryType GeometryType { get; set; }
}