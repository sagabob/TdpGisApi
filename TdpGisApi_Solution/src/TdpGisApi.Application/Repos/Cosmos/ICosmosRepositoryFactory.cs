using Microsoft.Azure.Cosmos;
using TdpGisApi.Application.Repos.Cosmos.Repos;

namespace TdpGisApi.Application.Repos.Cosmos;

public interface ICosmosRepositoryFactory
{
    CosmosRepository CreateRepository(CosmosClient cosmosClient, string databaseId, string collectionName);
}