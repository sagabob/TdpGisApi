using Newtonsoft.Json.Linq;

namespace TdpGisApi.Application.Handlers;

public interface IGisFeatureDataHandler
{
    Task<List<JObject>> GetFeatureDataByText(Guid featureId, string text);
}