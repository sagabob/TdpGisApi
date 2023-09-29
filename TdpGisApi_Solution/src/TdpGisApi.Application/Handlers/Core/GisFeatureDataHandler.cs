using Newtonsoft.Json.Linq;
using TdpGisApi.Application.Factory;
using TdpGisApi.Application.Models;
using TdpGisApi.Application.Response;

namespace TdpGisApi.Application.Handlers.Core;

public class GisFeatureDataHandler : IGisFeatureDataHandler
{
    private readonly IGisAppFactory _gisAppFactory;
    private readonly IGisFeatureDataCosmosHandler _gisFeatureDataCosmosHandler;

    public GisFeatureDataHandler(IGisAppFactory gisAppFactory, IGisFeatureDataCosmosHandler gisFeatureDataCosmosHandler)
    {
        _gisAppFactory = gisAppFactory;
        _gisFeatureDataCosmosHandler = gisFeatureDataCosmosHandler;
    }

    public async Task<ApiOkResponse<PagedList<JObject>>> GetFeatureDataByText(Guid featureId, string text)
    {
        var featureInfo = (await _gisAppFactory.CreateAppFeatureData()).Features.FirstOrDefault(x => x.Id == featureId);
        switch (featureInfo)
        {
            case null:
                throw new KeyNotFoundException("Not found the queried feature");
            case { Connection.DbType: DbType.Cosmosdb }:
            {
                try
                {
                    return await _gisFeatureDataCosmosHandler.GetFeatureDataByText(featureInfo, text);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            default:
                throw new NotImplementedException();
        }
    }


    public async Task<ApiOkResponse<PagedList<JObject>>> GetPagingFeatureDataByText(Guid featureId, string text, int pageSize, int pageNumber, string? token)
    {
        var featureInfo = (await _gisAppFactory.CreateAppFeatureData()).Features.FirstOrDefault(x => x.Id == featureId);
        switch (featureInfo)
        {
            case null:
                throw new KeyNotFoundException("Not found the queried feature");
            case { Connection.DbType: DbType.Cosmosdb }:
            {
                try
                {
                    return await _gisFeatureDataCosmosHandler.GetPagingFeatureDataByText(featureInfo, text, pageSize,pageNumber, token);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            default:
                throw new NotImplementedException();
        }
    }
}