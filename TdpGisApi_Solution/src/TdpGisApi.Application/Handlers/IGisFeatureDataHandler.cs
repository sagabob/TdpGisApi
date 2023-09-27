namespace TdpGisApi.Application.Handlers;

public interface IGisFeatureDataHandler
{
    Task<List<dynamic>> GetFeatureDataByText(Guid featureId, string text);
}