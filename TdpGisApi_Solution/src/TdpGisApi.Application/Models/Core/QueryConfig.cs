using System.ComponentModel.DataAnnotations;
using TdpGisApi.Core;

namespace TdpGisApi.Application.Models.Core;

public class QueryConfig : IEntity
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