using Newtonsoft.Json.Linq;
using TdpGisApi.Application.DataProviders.Cosmos;
using TdpGisApi.Application.Models;
using TdpGisApi.Application.Response;

namespace TdpGisApi.Application.Handlers.Core;

public class GisFeatureDataCosmosHandler : IGisFeatureDataCosmosHandler
{
    private readonly IComosClientFactory _comosClientFactory;
    private readonly ICosmosRepositoryFactory _cosmosRepositoryFactory;

    public GisFeatureDataCosmosHandler(IComosClientFactory comosClientFactory,
        ICosmosRepositoryFactory cosmosRepositoryFactory)
    {
        _comosClientFactory = comosClientFactory;
        _cosmosRepositoryFactory = cosmosRepositoryFactory;
    }

    public async Task<FeatureCollection> GetFeatureDataByText(QueryConfig featureInfo, string text)
    {
        var cosmosClient = _comosClientFactory.Create(featureInfo.Connection);
        var repos = _cosmosRepositoryFactory.CreateRepository(cosmosClient,
            featureInfo.Connection.DatabaseId,
            featureInfo.CollectionName);

        repos.GetContainer();

        var querySql = $"SELECT * FROM c WHERE c.{featureInfo.QueryField} like '%{text}%' ";

        var results = await repos.QuerySql(querySql, featureInfo);

        return results;
    }

    public async Task<FeatureCollection> GetAllFeatureData(QueryConfig featureInfo)
    {
        var cosmosClient = _comosClientFactory.Create(featureInfo.Connection);
        var repos = _cosmosRepositoryFactory.CreateRepository(cosmosClient,
            featureInfo.Connection.DatabaseId,
            featureInfo.CollectionName);

        repos.GetContainer();

        var querySql = $"SELECT * FROM c ORDER BY c.{featureInfo.QueryField}";

        var results = await repos.QuerySql(querySql, featureInfo);

        return results;
    }

    public async Task<FeatureCollection> GetPagingFeatureDataByText(QueryConfig featureInfo, string text,
        int pageSize, int pageNumber, string? token)
    {
        var cosmosClient = _comosClientFactory.Create(featureInfo.Connection);
        var repos = _cosmosRepositoryFactory.CreateRepository(cosmosClient,
            featureInfo.Connection.DatabaseId,
            featureInfo.CollectionName);

        repos.GetContainer();

        var querySql = $"SELECT * FROM c WHERE c.{featureInfo.QueryField} like '%{text}%' ";

        var results = await repos.QuerySqlWithPaging(querySql, featureInfo, pageSize, pageNumber, token);

        return results;
    }

    public async Task<Dictionary<string, FeatureCollection>> GetSpatialData(QueryConfig featureInfo, JObject boundaries)
    {
        var cosmosClient = _comosClientFactory.Create(featureInfo.Connection);
        var repos = _cosmosRepositoryFactory.CreateRepository(cosmosClient,
            featureInfo.Connection.DatabaseId,
            featureInfo.CollectionName);

        repos.GetContainer();

        var dicOfData = new Dictionary<string, FeatureCollection>();
        foreach (var f in boundaries)
        {
            var querySql = $"SELECT * FROM c WHERE ST_WITHIN(c.Location, {f.Value["geometry"]} ";

            var results = await repos.QuerySql(querySql, featureInfo);

            dicOfData.Add(boundaries.GetValue("id").ToString(), results);
        }


        return dicOfData;
    }


    public async Task<FeatureCollection> GetSpatialDataSingleBoundary(QueryConfig featureInfo, JObject boundaries)
    {
        var cosmosClient = _comosClientFactory.Create(featureInfo.Connection);
        var repos = _cosmosRepositoryFactory.CreateRepository(cosmosClient,
            featureInfo.Connection.DatabaseId,
            featureInfo.CollectionName);

        repos.GetContainer();

        var querySql = $"SELECT * FROM c WHERE ST_WITHIN(c.Location, {boundaries.GetValue("geometry")})";

        var results = await repos.QuerySql(querySql, featureInfo);


        return results;
    }
}