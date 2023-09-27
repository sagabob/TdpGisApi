using Newtonsoft.Json.Linq;
using TdpGisApi.Application.DataProviders.Cosmos;
using TdpGisApi.Application.Factory;
using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.Handlers.Core;

public class GisFeatureDataHandler: IGisFeatureDataHandler
{
    private readonly IComosClientFactory _comosClientFactory;
    private readonly ICosmosRepositoryFactory _cosmosRepositoryFactory;
    private readonly IGisAppFactory _gisAppFactory;

    public GisFeatureDataHandler(IGisAppFactory gisAppFactory, IComosClientFactory comosClientFactory,
        ICosmosRepositoryFactory cosmosRepositoryFactory)
    {
        _gisAppFactory = gisAppFactory;
        _comosClientFactory = comosClientFactory;
        _cosmosRepositoryFactory = cosmosRepositoryFactory;
    }

    public async Task<List<dynamic>> GetFeatureDataByText(Guid featureId, string text)
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
                    var cosmosClient = _comosClientFactory.Create(featureInfo.Connection);
                    var repos = _cosmosRepositoryFactory.CreateRepository(cosmosClient, featureInfo.Connection.DatabaseId,
                        featureInfo.CollectionName);

                    repos.GetContainer();

                    var queryableFeatures = await repos.Query<dynamic>(featureInfo.PartitionKey!);
                    var result = queryableFeatures.Take(10).ToList();

                    return result;
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