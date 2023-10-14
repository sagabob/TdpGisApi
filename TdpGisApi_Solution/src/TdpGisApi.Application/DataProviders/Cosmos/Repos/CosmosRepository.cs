using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json.Linq;
using TdpGisApi.Application.Models;
using TdpGisApi.Application.Models.Core;
using TdpGisApi.Application.Response;

namespace TdpGisApi.Application.DataProviders.Cosmos.Repos;

public class CosmosRepository : ICosmosRepository
{
    private readonly string _collectionName;
    private readonly CosmosClient _cosmosClient;
    private readonly string _databaseId;
    private readonly ICosmosQueryHelpers _queryHelpers;
    private Container _cosmosContainer = null!;

    public CosmosRepository(ICosmosQueryHelpers queryHelpers, CosmosClient cosmosClient, string databaseId,
        string collectionName
    )
    {
        _queryHelpers = queryHelpers;
        _cosmosClient = cosmosClient;
        _databaseId = databaseId;
        _collectionName = collectionName;
    }

    public async Task<FeatureCollection> QuerySql(string sql, IPropertyMapping featureConfig)
    {
        var query = _queryHelpers.GetQueryDefinition(sql);

        var results = new List<JObject>();
        var iterator =
            _cosmosContainer.GetItemQueryIterator<dynamic>(query, null, new QueryRequestOptions { MaxItemCount = 50 });


        while (iterator.HasMoreResults)
        {
            var documents = await iterator.ReadNextAsync();

            foreach (var document in documents)
                results.Add(_queryHelpers.OutputSingleFeatureMapping(document, featureConfig));
        }

        var featureCollection = new FeatureCollection(featureConfig.Id, results, -1, 50, null);

        return featureCollection;
    }


    public async Task<FeatureCollection> QuerySqlWithPaging(string sql, string totalCountSql, QueryConfig featureConfig,
        int pageSize, int currentPageNumber, string? continuationToken = null)

    {
        var query = _queryHelpers.GetQueryDefinition(sql);

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
                results.Add(_queryHelpers.OutputSingleFeatureMapping(document, featureConfig));

            if (documents.Count > 0)
            {
                token = !string.IsNullOrEmpty(documents.ContinuationToken)
                    ? WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(documents.ContinuationToken))
                    : null;
                break;
            }
        }

        var featureCollection = new FeatureCollection(featureConfig.Id, results, currentPageNumber, pageSize, token);

        if (currentPageNumber != 1) return featureCollection;

        if (token == null)
        {
            featureCollection = new FeatureCollection(featureConfig.Id, results, currentPageNumber, pageSize,
                results.Count, token);
        }
        else
        {
            var calculatedTotal = await QueryTotalCountSql(totalCountSql);

            if (calculatedTotal > 0)
                featureCollection = new FeatureCollection(featureConfig.Id, results, currentPageNumber,
                    pageSize, calculatedTotal, token);
        }


        return featureCollection;
    }

    public async Task<int> QueryTotalCountSql(string sql)
    {
        // remove \r\n and whitespace
        var query = new QueryDefinition(Regex.Replace(sql, @"\s+", " ").Trim());


        var iterator =
            _cosmosContainer.GetItemQueryIterator<dynamic>(query, null, new QueryRequestOptions { MaxItemCount = 50 });

        var totalCount = -1;
        while (iterator.HasMoreResults)
        {
            var documents = await iterator.ReadNextAsync();

            foreach (var document in documents)
            {
                totalCount = document["$1"];
                break;
            }

            break;
        }

        return totalCount;
    }


    public void GetContainer()
    {
        _cosmosContainer = _cosmosClient.GetContainer(_databaseId, _collectionName);
    }
}