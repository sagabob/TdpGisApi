using Newtonsoft.Json.Linq;
using TdpGisApi.Application.Models;
using TdpGisApi.Application.Response;

namespace TdpGisApi.Application.Handlers;

public interface IGisFeatureDataCosmosHandler
{
    Task<ApiOkResponse<PagedList<JObject>>> GetFeatureDataByText(QueryConfig featureInfo, string text);

    Task<ApiOkResponse<PagedList<JObject>>> GetPagingFeatureDataByText(QueryConfig featureInfo, string text,
        int pageSize, int pageNumber, string token);
}