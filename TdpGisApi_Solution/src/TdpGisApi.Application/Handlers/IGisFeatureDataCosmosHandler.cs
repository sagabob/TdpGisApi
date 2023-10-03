using Newtonsoft.Json.Linq;
using TdpGisApi.Application.Models;
using TdpGisApi.Application.Response;

namespace TdpGisApi.Application.Handlers;

public interface IGisFeatureDataCosmosHandler
{
    Task<FeatureCollection> GetFeatureDataByText(QueryConfig featureInfo, string text);

    Task<FeatureCollection> GetPagingFeatureDataByText(QueryConfig featureInfo, string text,
        int pageSize, int pageNumber, string? token);

    Task<FeatureCollection> GetAllFeatureData(QueryConfig featureInfo);

    Task<Dictionary<string, FeatureCollection>> GetSpatialData(QueryConfig featureInfo, JObject boundaries);

    Task<FeatureCollection> GetSpatialDataSingleBoundary(QueryConfig featureInfo, JObject boundaries);
}