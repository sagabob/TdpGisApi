using Microsoft.Azure.Cosmos;
using TdpGisApi.Application.DataProviders.Cosmos.Repos;

namespace TdpGisApi.Application.DataProviders.Cosmos;

public interface ICosmosRepositoryFactory
{
    CosmosRepository CreateRepository(ICosmosQueryHelpers cosmosQueryHelpers, CosmosClient cosmosClient,
        string databaseId, string collectionName);
}