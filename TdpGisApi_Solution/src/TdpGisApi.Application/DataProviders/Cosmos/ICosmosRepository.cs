using Newtonsoft.Json.Linq;
using TdpGisApi.Application.Models;
using TdpGisApi.Application.Response;

namespace TdpGisApi.Application.DataProviders.Cosmos;

public interface ICosmosRepository
{
    Task<ApiOkResponse<PagedList<JObject>>> QuerySql(string sql, QueryConfig featureConfig);

    Task<ApiOkResponse<PagedList<JObject>>> QuerySqlWithPaging(string sql, QueryConfig featureConfig,
        int pageSize, int currentPageNumber, string? continuationToken = null);
}