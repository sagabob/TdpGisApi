using Newtonsoft.Json.Linq;
using TdpGisApi.Application.Response;

namespace TdpGisApi.Application.Handlers;

public interface IGisFeatureDataHandler
{
    Task<ApiOkResponse<PagedList<JObject>>> GetFeatureDataByText(Guid featureId, string text);

    Task<ApiOkResponse<PagedList<JObject>>> GetPagingFeatureDataByText(Guid featureId, string text, int pageSize,
        int pageNumber, string? token);
}