using Newtonsoft.Json.Linq;
using TdpGisApi.Application.DataProviders.Cosmos;
using TdpGisApi.Application.Models.Core;
using TdpGisApi.Application.Response;

namespace TdpGisApi.Application.Handlers.Core;

public class GisFeatureDataCosmosHandler : IGisFeatureDataCosmosHandler
{
    private readonly IComosClientFactory _cosmosClientFactory;
    private readonly ICosmosQueryHelpers _cosmosQueryHelpers;
    private readonly ICosmosRepositoryFactory _cosmosRepositoryFactory;

    public GisFeatureDataCosmosHandler(IComosClientFactory cosmosClientFactory,
        ICosmosRepositoryFactory cosmosRepositoryFactory, ICosmosQueryHelpers cosmosQueryHelpers)
    {
        _cosmosClientFactory = cosmosClientFactory;
        _cosmosRepositoryFactory = cosmosRepositoryFactory;
        _cosmosQueryHelpers = cosmosQueryHelpers;
    }

    public async Task<FeatureCollection> GetFeatureDataByText(QueryConfig featureInfo, string text)
    {
        var cosmosClient = _cosmosClientFactory.Create(featureInfo.Connection);
        var repos = _cosmosRepositoryFactory.CreateRepository(_cosmosQueryHelpers, cosmosClient,
            featureInfo.Connection.DatabaseId,
            featureInfo.CollectionName);

        repos.GetContainer();

        var querySql = $"SELECT * FROM c WHERE c.{featureInfo.QueryField} like '%{text}%' ";

        var results = await repos.QuerySql(querySql, featureInfo);

        return results;
    }

    public async Task<FeatureCollection> GetAllFeatureData(QueryConfig featureInfo)
    {
        var cosmosClient = _cosmosClientFactory.Create(featureInfo.Connection);
        var repos = _cosmosRepositoryFactory.CreateRepository(_cosmosQueryHelpers, cosmosClient,
            featureInfo.Connection.DatabaseId,
            featureInfo.CollectionName);

        repos.GetContainer();

        var querySql = $"SELECT * FROM c ORDER BY c.{featureInfo.QueryField}";

        var results = await repos.QuerySql(querySql, featureInfo);

        return results;
    }

    public async Task<FeatureCollection> GetAllLayerData(FeatureLayer featureInfo)
    {
        var cosmosClient = _cosmosClientFactory.Create(featureInfo.Connection);
        var repos = _cosmosRepositoryFactory.CreateRepository(_cosmosQueryHelpers, cosmosClient,
            featureInfo.Connection.DatabaseId,
            featureInfo.CollectionName);

        repos.GetContainer();

        var querySql = "SELECT * FROM c ";

        var results = await repos.QuerySql(querySql, featureInfo);

        return results;
    }

    public async Task<FeatureCollection> GetPagingFeatureDataByText(QueryConfig featureInfo, string text,
        int pageSize, int pageNumber, string? token)
    {
        var cosmosClient = _cosmosClientFactory.Create(featureInfo.Connection);
        var repos = _cosmosRepositoryFactory.CreateRepository(_cosmosQueryHelpers, cosmosClient,
            featureInfo.Connection.DatabaseId,
            featureInfo.CollectionName);

        repos.GetContainer();

        var querySql = $"SELECT * FROM c WHERE c.{featureInfo.QueryField} like '%{text}%'";

        var querySqlTotalCount = $"SELECT Count(1) FROM c WHERE c.{featureInfo.QueryField} like '%{text}%'";

        var results =
            await repos.QuerySqlWithPaging(querySql, querySqlTotalCount, featureInfo, pageSize, pageNumber, token);

        return results;
    }

    public async Task<Dictionary<string, FeatureCollection>> GetSpatialData(QueryConfig featureInfo, JObject boundaries)
    {
        var cosmosClient = _cosmosClientFactory.Create(featureInfo.Connection);
        var repos = _cosmosRepositoryFactory.CreateRepository(_cosmosQueryHelpers, cosmosClient,
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
        var cosmosClient = _cosmosClientFactory.Create(featureInfo.Connection);
        var repos = _cosmosRepositoryFactory.CreateRepository(_cosmosQueryHelpers, cosmosClient,
            featureInfo.Connection.DatabaseId,
            featureInfo.CollectionName);

        repos.GetContainer();

        var querySql = $"SELECT * FROM c WHERE ST_WITHIN(c.Location, {boundaries.GetValue("geometry")})";

        var results = await repos.QuerySql(querySql, featureInfo);

        return results;
    }
}