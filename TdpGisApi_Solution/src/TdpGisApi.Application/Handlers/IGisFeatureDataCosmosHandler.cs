using TdpGisApi.Application.Models;
using TdpGisApi.Application.Response;

namespace TdpGisApi.Application.Handlers;

public interface IGisFeatureDataCosmosHandler
{
    Task<ApiOkResponse<FeatureCollection>> GetFeatureDataByText(QueryConfig featureInfo, string text);

    Task<ApiOkResponse<FeatureCollection>> GetPagingFeatureDataByText(QueryConfig featureInfo, string text,
        int pageSize, int pageNumber, string? token);
}