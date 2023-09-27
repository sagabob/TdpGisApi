using System.Text.RegularExpressions;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json.Linq;
using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.DataProviders.Cosmos.Repos;

public class CosmosRepository : ICosmosRepository
{
    private static readonly CosmosLinqSerializerOptions CosmosLinqSerializerOptions =
        new() { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase };

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


    public async Task<List<JObject>> QuerySql(string sql, QueryConfig featureConfig)

    {
        // remove \r\n and whitespace
        var query = new QueryDefinition(Regex.Replace(sql, @"\s+", " ").Trim());

        var results = new List<JObject>();
        var iterator = _cosmosContainer.GetItemQueryIterator<dynamic>(query);


        while (iterator.HasMoreResults)
        {
            var documents = await iterator.ReadNextAsync();
            foreach (var document in documents)
            {
                var jsonItem = new JObject { { "id", document.id } };

                foreach (var map in featureConfig.Mappings.Where(map => document[map.PropertyName] != null))
                    jsonItem.Add(map.OutputName, document[map.PropertyName]);

                results.Add(jsonItem);
            }
        }

        return results;
    }


    public void GetContainer()
    {
        _cosmosContainer = _cosmosClient.GetContainer(_databaseId, _collectionName);
    }
}