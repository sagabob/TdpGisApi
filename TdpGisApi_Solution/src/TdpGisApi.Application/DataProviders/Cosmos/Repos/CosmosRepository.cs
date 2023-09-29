using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json.Linq;
using TdpGisApi.Application.Models;
using TdpGisApi.Application.Response;

namespace TdpGisApi.Application.DataProviders.Cosmos.Repos;

public class CosmosRepository : ICosmosRepository
{
    private readonly string _collectionName;
    private readonly CosmosClient _cosmosClient;
    private readonly string _databaseId;
    private Container _cosmosContainer = null!;

    public CosmosRepository(CosmosClient cosmosClient, string databaseId, string collectionName
    )
    {
        _cosmosClient = cosmosClient;
        _databaseId = databaseId;
        _collectionName = collectionName;
    }


    public async Task<ApiOkResponse<FeatureCollection>> QuerySql(string sql, QueryConfig featureConfig)

    {
        // remove \r\n and whitespace
        var query = new QueryDefinition(Regex.Replace(sql, @"\s+", " ").Trim());

        var results = new List<JObject>();
        var iterator =
            _cosmosContainer.GetItemQueryIterator<dynamic>(query, null, new QueryRequestOptions { MaxItemCount = 50 });


        while (iterator.HasMoreResults)
        {
            var documents = await iterator.ReadNextAsync();

            foreach (var document in documents)
            {
                var jsonFeature = new JObject { { "type", "Feature" } };

                var jsonProperties = new JObject();

                foreach (var map in featureConfig.Mappings.Where(map => document[map.PropertyName] != null))
                    if (map.PropertyType == PropertyType.Spatial)
                        jsonFeature.Add(map.OutputName, document[map.PropertyName]);
                    else
                        jsonProperties.Add(map.OutputName, document[map.PropertyName]);

                jsonFeature.Add("properties", jsonProperties);

                results.Add(jsonFeature);
            }
        }

        var featureCollection = new FeatureCollection(featureConfig.Id, results, -1, 50, null);

        return new ApiOkResponse<FeatureCollection>(featureCollection);
    }

    public async Task<ApiOkResponse<FeatureCollection>> QuerySqlWithPaging(string sql, QueryConfig featureConfig,
        int pageSize, int currentPageNumber, string? continuationToken = null)

    {
        // remove \r\n and whitespace
        var query = new QueryDefinition(Regex.Replace(sql, @"\s+", " ").Trim());

        string? token = null;

        var inputToken = string.IsNullOrEmpty(continuationToken)
            ? null
            : Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(continuationToken));

        var results = new List<JObject>();
        var iterator =
            _cosmosContainer.GetItemQueryIterator<dynamic>(query,
                inputToken,
                new QueryRequestOptions { MaxItemCount = pageSize });


        while (iterator.HasMoreResults)
        {
            var documents = await iterator.ReadNextAsync();

            foreach (var document in documents)
            {
                var jsonFeature = new JObject { { "type", "Feature" } };

                var jsonProperties = new JObject();

                foreach (var map in featureConfig.Mappings.Where(map => document[map.PropertyName] != null))
                    if (map.PropertyType == PropertyType.Spatial)
                        jsonFeature.Add(map.OutputName, document[map.PropertyName]);
                    else
                        jsonProperties.Add(map.OutputName, document[map.PropertyName]);

                jsonFeature.Add("properties", jsonProperties);

                results.Add(jsonFeature);
            }

            if (documents.Count > 0)
            {
                token = !string.IsNullOrEmpty(documents.ContinuationToken)
                    ? WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(documents.ContinuationToken))
                    : null;
                break;
            }
        }

        var featureCollection = new FeatureCollection(featureConfig.Id, results, currentPageNumber, pageSize, token);

        return new ApiOkResponse<FeatureCollection>(featureCollection);
    }


    public void GetContainer()
    {
        _cosmosContainer = _cosmosClient.GetContainer(_databaseId, _collectionName);
    }
}