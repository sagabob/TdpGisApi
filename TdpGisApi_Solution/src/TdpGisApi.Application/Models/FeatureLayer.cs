namespace TdpGisApi.Application.Models;

public class FeatureLayer
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string DisplayName { get; set; }

    public required string CollectionName { get; set; }

    public string? PartitionKey { get; set; }

    public string? Description { get; set; }

    public required List<PropertyOutput> Mappings { get; set; }

    public required ConnectionObject Connection { get; set; }

    public GeometryType GeometryType { get; set; }

    public required bool IsDisabled { get; set; }

    public required ShowLevel ShowLevel { get; set; }
}