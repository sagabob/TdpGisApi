using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

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

    public Task<IOrderedQueryable<T>> Query<T>(string partitionKey)
    {
        return Task.FromResult(_cosmosContainer.GetItemLinqQueryable<T>(
            requestOptions: new QueryRequestOptions { PartitionKey = new PartitionKey(partitionKey) },
            linqSerializerOptions: CosmosLinqSerializerOptions));
    }

    public async Task<List<T>> QuerySql<T>(string sql, object paramValues, string partitionKey) where T : class
    {
        // remove \r\n and whitespace
        var query = new QueryDefinition(Regex.Replace(sql, @"\s+", " ").Trim());

        var dictionary = paramValues.GetType().GetProperties()
            .ToDictionary(x => x.Name, x => x.GetValue(paramValues)?.ToString() ?? "");
        foreach (var kvp in dictionary) query.WithParameter("@" + kvp.Key, kvp.Value);

        var results = new List<T>();
        using var resultSetIterator = _cosmosContainer.GetItemQueryIterator<T>(
            query,
            requestOptions: new QueryRequestOptions
            {
                PartitionKey = new PartitionKey(partitionKey)
            });

        while (resultSetIterator.HasMoreResults)
        {
            var response = await resultSetIterator.ReadNextAsync();
            results.AddRange(response.Resource);
        }

        return results;
    }

    public async Task<CosmosRepositoryResult<T>> Delete<T>(string id, string partitionKey)
        where T : class
    {
        ItemResponse<T> response = null!;
        try
        {
            response = await _cosmosContainer.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
            return new CosmosRepositoryResult<T>(response.StatusCode, response.Resource, string.Empty);
        }
        catch (CosmosException ex)
        {
            return new CosmosRepositoryResult<T>(response?.StatusCode ?? HttpStatusCode.BadRequest, null!, ex.Message);
        }
    }

    public async Task<CosmosRepositoryResult<T>> Upsert<T>(string partitionKey, T model)
        where T : class
    {
        ItemResponse<T> response = null!;
        try
        {
            // result.StatusCode == System.Net.HttpStatusCode.OK || historyResult.StatusCode == System.Net.HttpStatusCode.Created
            response = await _cosmosContainer.UpsertItemAsync(partitionKey: new PartitionKey(partitionKey),
                item: model);
            return new CosmosRepositoryResult<T>(response.StatusCode, response.Resource, string.Empty);
        }
        catch (CosmosException ex)
        {
            return new CosmosRepositoryResult<T>(response?.StatusCode ?? HttpStatusCode.BadRequest, null!, ex.Message);
        }
    }

    public void GetContainer()
    {
        _cosmosContainer = _cosmosClient.GetContainer(_databaseId, _collectionName);
    }

    public static async Task<List<T>> LinqQueryToResults<T>(IQueryable<T> queryable)
    {
        var results = new List<T>();
        using var iterator = queryable.ToFeedIterator();
        while (iterator.HasMoreResults) results.AddRange(await iterator.ReadNextAsync());

        return results;
    }
}