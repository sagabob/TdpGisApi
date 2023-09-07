using System.ComponentModel.DataAnnotations;

namespace TdpGisApi.Core.Models;

public class QueryConfig
{
    [Key] public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public QueryType QueryType { get; set; }

    public required string QueryField { get; set; }

    public required List<PropertyOutput> Mappings { get; set; }

    public required string ConnectionName { get; set; }

    public GeometryType GeometryType { get; set; }
}