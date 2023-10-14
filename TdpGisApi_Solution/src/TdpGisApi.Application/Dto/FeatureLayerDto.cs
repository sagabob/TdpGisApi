using TdpGisApi.Application.Models.Core;

namespace TdpGisApi.Application.Dto;

public class FeatureLayerDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string DisplayName { get; set; }

    public required string CollectionName { get; set; }

    public string? PartitionKey { get; set; }

    public string? Description { get; set; }

    public required List<PropertyOutput> Mappings { get; set; }
    public GeometryType GeometryType { get; set; }
    public Guid ConnectionId { get; set; }

    public required StyleLayer Style { get; set; }
}