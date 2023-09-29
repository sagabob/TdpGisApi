using System.Text;
using System.Text.RegularExpressions;
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


    public async Task<ApiOkResponse<PagedList<JObject>>> QuerySql(string sql, QueryConfig featureConfig)

    {
        // remove \r\n and whitespace
        var query = new QueryDefinition(Regex.Replace(sql, @"\s+", " ").Trim());

        var results = new List<JObject>();
        var iterator =
            _cosmosContainer.GetItemQueryIterator<dynamic>(query, null, new QueryRequestOptions { MaxItemCount = 100 });


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

        var pageList = new PagedList<JObject>(results, -1, 100, null);

        return new ApiOkResponse<PagedList<JObject>>(pageList);
    }

    public async Task<ApiOkResponse<PagedList<JObject>>> QuerySqlWithPaging(string sql, QueryConfig featureConfig,
        int pageSize, int currentPageNumber, string? continuationToken = null)

    {
        // remove \r\n and whitespace
        var query = new QueryDefinition(Regex.Replace(sql, @"\s+", " ").Trim());

        string? token = null;


        var results = new List<JObject>();
        var iterator =
            _cosmosContainer.GetItemQueryIterator<dynamic>(query,
                string.IsNullOrEmpty(continuationToken)
                    ? null
                    : Encoding.UTF8.GetString(Convert.FromBase64String(continuationToken)),
                new QueryRequestOptions { MaxItemCount = pageSize });


        while (iterator.HasMoreResults)
        {
            var documents = await iterator.ReadNextAsync();
            if (token != null)
            {
                token = documents.Count > 0 ? token : null;
                break;
            }


            foreach (var document in documents)
            {
                var jsonItem = new JObject { { "id", document.id } };

                foreach (var map in featureConfig.Mappings.Where(map => document[map.PropertyName] != null))
                    jsonItem.Add(map.OutputName, document[map.PropertyName]);

                results.Add(jsonItem);
            }

            if (documents.Count > 0)
                token = Convert.ToBase64String(Encoding.UTF8.GetBytes(documents.ContinuationToken));
        }

        var pageList = new PagedList<JObject>(results, currentPageNumber, pageSize, token);


        return new ApiOkResponse<PagedList<JObject>>(pageList);
    }


    public void GetContainer()
    {
        _cosmosContainer = _cosmosClient.GetContainer(_databaseId, _collectionName);
    }
}