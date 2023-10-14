namespace TdpGisApi.Application.Models.Core;

public class FeatureLayer : IPropertyMapping
{
    public required string Name { get; set; }

    public required string DisplayName { get; set; }

    public required string CollectionName { get; set; }

    public string? PartitionKey { get; set; }

    public string? Description { get; set; }

    public required ConnectionObject Connection { get; set; }

    public GeometryType GeometryType { get; set; }

    public required bool IsDisabled { get; set; }

    public required ShowLevel ShowLevel { get; set; }

    public required StyleLayer Style { get; set; }
    public Guid Id { get; set; }

    public required List<PropertyOutput> Mappings { get; set; }
}