using TdpGisApi.Application.Configuration;

namespace TdpGisApi.Application.Factory;

public interface IGisAppFactory
{
    Task<AppFeatureData> CreateAppFeatureData();
}