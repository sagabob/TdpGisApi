using TdpGisApi.Application.Models;
using TdpGisApi.Application.Models.Core;
using TdpGisApi.Application.Response;

namespace TdpGisApi.Application.DataProviders.Cosmos;

public interface ICosmosRepository
{
    Task<FeatureCollection> QuerySql(string sql, IPropertyMapping featureConfig);

    Task<FeatureCollection> QuerySqlWithPaging(string sql, string totalCountSql, QueryConfig featureConfig,
        int pageSize, int currentPageNumber, string? continuationToken = null);
}