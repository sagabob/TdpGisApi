using Microsoft.Azure.Cosmos;
using TdpGisApi.Application.Repos.Cosmos.Repos;

namespace TdpGisApi.Application.Repos.Cosmos.Factory;

public class CosmosRepositoryFactory : ICosmosRepositoryFactory
{
    public CosmosRepository CreateRepository(CosmosClient cosmosClient, string databaseId, string collectionName)
    {
        return new CosmosRepository(cosmosClient, databaseId, collectionName);
    }
} 