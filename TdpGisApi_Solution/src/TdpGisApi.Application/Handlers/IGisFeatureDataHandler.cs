using Newtonsoft.Json.Linq;
using TdpGisApi.Application.Response;

namespace TdpGisApi.Application.Handlers;

public interface IGisFeatureDataHandler
{
    Task<ApiOkResponse<FeatureCollection>> GetFeatureDataByText(Guid featureId, string text);

    Task<ApiOkResponse<FeatureCollection>> GetPagingFeatureDataByText(Guid featureId, string text, int pageSize,
        int pageNumber, string? token);

    Task<ApiOkResponse<FeatureCollection>> GetAllFeatureData(Guid featureId);

    Task<ApiOkResponse<Dictionary<string, FeatureCollection>>> GetSpatialData(Guid featureId,
        JObject boundaries);

    Task<ApiOkResponse<FeatureCollection>> GetSpatialDataSingleBoundary(Guid featureId,
        JObject boundaries);
}