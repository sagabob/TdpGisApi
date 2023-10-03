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

    public async Task<ApiOkResponse<FeatureCollection>> GetFeatureDataByText(Guid featureId, string text)
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
                    var results = await _gisFeatureDataCosmosHandler.GetFeatureDataByText(featureInfo, text);
                    return new ApiOkResponse<FeatureCollection>(results);
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

    public async Task<ApiOkResponse<FeatureCollection>> GetAllFeatureData(Guid featureId)
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
                    var results = await _gisFeatureDataCosmosHandler.GetAllFeatureData(featureInfo);
                    return new ApiOkResponse<FeatureCollection>(results);
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


    public async Task<ApiOkResponse<FeatureCollection>> GetPagingFeatureDataByText(Guid featureId, string text,
        int pageSize, int pageNumber, string? token)
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
                    var results = await _gisFeatureDataCosmosHandler.GetPagingFeatureDataByText(featureInfo, text,
                        pageSize,
                        pageNumber, token);

                    return new ApiOkResponse<FeatureCollection>(results);
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


    public async Task<ApiOkResponse<Dictionary<string, FeatureCollection>>> GetSpatialData(Guid featureId,
        JObject boundaries)
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
                    var results = await _gisFeatureDataCosmosHandler.GetSpatialData(featureInfo, boundaries);

                    return new ApiOkResponse<Dictionary<string, FeatureCollection>>(results);
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

    public async Task<ApiOkResponse<FeatureCollection>> GetSpatialDataSingleBoundary(Guid featureId,
        JObject boundaries)
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
                    var results =
                        await _gisFeatureDataCosmosHandler.GetSpatialDataSingleBoundary(featureInfo, boundaries);

                    return new ApiOkResponse<FeatureCollection>(results);
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