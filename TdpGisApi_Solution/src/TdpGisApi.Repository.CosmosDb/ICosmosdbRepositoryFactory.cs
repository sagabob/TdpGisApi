using Microsoft.Azure.Cosmos;
using TdpGisApi.Application.Services.Core;

namespace TdpGisApi.Application.Factory;

public interface ICosmosdbRepositoryFactory
{
    CosmosdbRepository CreateRepository(CosmosClient cosmosClient, string databaseId, string collectionName);
}