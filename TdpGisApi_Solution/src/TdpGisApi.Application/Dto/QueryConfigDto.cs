using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.Dto;

public class QueryConfigDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string DisplayName { get; set; }

    public required string CollectionName { get; set; }

    public string? PartitionKey { get; set; }

    public string? Description { get; set; }

    public QueryType QueryType { get; set; }

    public required string QueryField { get; set; }

    public required List<PropertyOutput> Mappings { get; set; }
    public GeometryType GeometryType { get; set; }
    public Guid ConnectionId { get; set; }
}