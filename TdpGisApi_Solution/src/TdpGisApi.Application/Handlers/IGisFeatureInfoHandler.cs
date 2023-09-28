using TdpGisApi.Application.Dto;

namespace TdpGisApi.Application.Handlers;

public interface IGisFeatureInfoHandler
{
    Task<List<QueryConfigDto>> GetFeatureDtos();
    Task<List<QueryConfigLite>> GetFeatureLite();
}