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

    public async Task<ApiOkResponse<FeatureCollection>> GetFeatureDataByText(QueryConfig featureInfo, string text)
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

    public async Task<ApiOkResponse<FeatureCollection>> GetPagingFeatureDataByText(QueryConfig featureInfo, string text,
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
}