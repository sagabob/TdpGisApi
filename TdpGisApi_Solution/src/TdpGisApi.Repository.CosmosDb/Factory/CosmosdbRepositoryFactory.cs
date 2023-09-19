using Microsoft.Azure.Cosmos;
using TdpGisApi.Application.Factory;
using TdpGisApi.Repository.CosmosDb.Repos;

namespace TdpGisApi.Repository.CosmosDb.Factory;

public class CosmosdbRepositoryFactory : ICosmosdbRepositoryFactory
{
    public CosmosdbRepository CreateRepository(CosmosClient cosmosClient, string databaseId, string collectionName)
    {
        return new CosmosdbRepository(cosmosClient, databaseId, collectionName);
    }
}