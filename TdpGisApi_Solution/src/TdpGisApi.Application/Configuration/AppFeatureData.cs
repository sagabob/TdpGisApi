using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.Configuration;

public class AppFeatureData
{
    public List<ConnectionObject> Connections { get; set; } = new();

    public List<QueryConfig> Features { get; set; } = new();

    public List<FeatureLayer> Layers { get; set; } = new();
}