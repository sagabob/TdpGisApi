using TdpGisApi.Application.Models.Core;

namespace TdpGisApi.Application.Dto;

public class FeatureLayerLite
{
    public Guid Id { get; set; }

    public required string DisplayName { get; set; }

    public GeometryType GeometryType { get; set; }
}