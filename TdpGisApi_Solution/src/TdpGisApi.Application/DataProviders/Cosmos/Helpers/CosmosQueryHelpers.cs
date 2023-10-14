using System.Text.RegularExpressions;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json.Linq;
using TdpGisApi.Application.Models;
using TdpGisApi.Application.Models.Core;

namespace TdpGisApi.Application.DataProviders.Cosmos.Helpers;

public class CosmosQueryHelpers : ICosmosQueryHelpers
{
    public QueryDefinition GetQueryDefinition(string sql)
    {
        return new QueryDefinition(Regex.Replace(sql, @"\s+", " ").Trim());
    }

    public JObject OutputSingleFeatureMapping(dynamic outputFeature, IPropertyMapping featureConfig)
    {
        var jsonFeature = new JObject { { "type", "Feature" } };

        var jsonProperties = new JObject();

        foreach (var map in featureConfig.Mappings.Where(map => outputFeature[map.PropertyName] != null))
            if (map.PropertyType == PropertyType.Spatial)
                jsonFeature.Add(map.OutputName, outputFeature[map.PropertyName]);
            else
                jsonProperties.Add(map.OutputName, outputFeature[map.PropertyName]);

        jsonFeature.Add("properties", jsonProperties);

        return jsonFeature;
    }
}