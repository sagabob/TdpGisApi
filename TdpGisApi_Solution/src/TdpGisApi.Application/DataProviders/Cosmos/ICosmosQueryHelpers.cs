using Microsoft.Azure.Cosmos;
using Newtonsoft.Json.Linq;
using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.DataProviders.Cosmos;

public interface ICosmosQueryHelpers
{
    QueryDefinition GetQueryDefinition(string sql);

    JObject OutputSingleFeatureMapping(dynamic outputFeature, IPropertyMapping featureConfig);
}