using Microsoft.Azure.Cosmos;
using TdpGisApi.Repository.CosmosDb.Repos;

namespace TdpGisApi.Repository.CosmosDb.Factory;

public class CosmosRepositoryFactory : ICosmosRepositoryFactory
{
    public CosmosRepository CreateRepository(CosmosClient cosmosClient, string databaseId, string collectionName)
    {
        return new CosmosRepository(cosmosClient, databaseId, collectionName);
    }
} 