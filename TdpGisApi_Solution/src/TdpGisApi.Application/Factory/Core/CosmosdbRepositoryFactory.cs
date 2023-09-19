using Microsoft.Azure.Cosmos;
using TdpGisApi.Application.Services.Core;

namespace TdpGisApi.Application.Factory.Core;

public class CosmosdbRepositoryFactory: ICosmosdbRepositoryFactory
{
    public CosmosdbRepository CreateRepository(CosmosClient cosmosClient, string databaseId, string collectionName)
    {
        return new CosmosdbRepository(cosmosClient, databaseId, collectionName);
    }
}