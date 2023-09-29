using TdpGisApi.Application.Models;
using TdpGisApi.Application.Response;

namespace TdpGisApi.Application.DataProviders.Cosmos;

public interface ICosmosRepository
{
    Task<ApiOkResponse<FeatureCollection>> QuerySql(string sql, QueryConfig featureConfig);

    Task<ApiOkResponse<FeatureCollection>> QuerySqlWithPaging(string sql, QueryConfig featureConfig,
        int pageSize, int currentPageNumber, string? continuationToken = null);
}