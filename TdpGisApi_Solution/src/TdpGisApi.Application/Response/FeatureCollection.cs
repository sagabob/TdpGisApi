using Newtonsoft.Json.Linq;

namespace TdpGisApi.Application.Response;

public class FeatureCollection
{
    public FeatureCollection(Guid featureId, List<JObject> items)
    {
        Features = items;
        Properties = new JObject { { "featureId", featureId } };
    }

    public FeatureCollection(Guid featureId, List<JObject> items, int pageNumber, int pageSize, string? token)
    {
        Features = items;
        Properties = new JObject
            { { "featureId", featureId }, { "pageNumber", pageNumber }, { "pageSize", pageSize }, { "token", token } };
    }

    public FeatureCollection(Guid featureId, List<JObject> items, int pageNumber, int pageSize, int totalCount,
        string? token)
    {
        Features = items;
        Properties = new JObject
        {
            { "featureId", featureId }, { "pageNumber", pageNumber }, { "pageSize", pageSize },
            { "totalCount", totalCount }, { "token", token }
        };
    }

    public string Type => nameof(FeatureCollection);
    public List<JObject> Features { get; private set; }
    public JObject Properties { get; private set; }
}