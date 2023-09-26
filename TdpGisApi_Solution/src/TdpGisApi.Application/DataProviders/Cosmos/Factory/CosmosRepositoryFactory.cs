using Microsoft.Azure.Cosmos;
using TdpGisApi.Application.DataProviders.Cosmos.Repos;

namespace TdpGisApi.Application.DataProviders.Cosmos.Factory;

public class CosmosRepositoryFactory : ICosmosRepositoryFactory
{
    public CosmosRepository CreateRepository(CosmosClient cosmosClient, string databaseId, string collectionName)
    {
        return new CosmosRepository(cosmosClient, databaseId, collectionName);
    }
}