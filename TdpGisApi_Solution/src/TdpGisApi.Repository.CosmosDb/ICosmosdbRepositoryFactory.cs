using Microsoft.Azure.Cosmos;
using TdpGisApi.Repository.CosmosDb.Repos;

namespace TdpGisApi.Application.Factory;

public interface ICosmosdbRepositoryFactory
{
    CosmosdbRepository CreateRepository(CosmosClient cosmosClient, string databaseId, string collectionName);
}