using Microsoft.Azure.Cosmos;
using TdpGisApi.Application.Models;

namespace TdpGisApi.Repository.CosmosDb.Factory;

public class ComosClientFactory : IComosClientFactory
{
    public CosmosClient Create(ConnectionObject connectionObject)
    {
        if (connectionObject.ConnectionType == ConnectionType.ConnectionString)
            return new CosmosClient(connectionObject.ConnectionString);

        //TODO Will implement how to create CosmosClient with RBAC credentials
        return new CosmosClient(connectionObject.ConnectionString);
    }
}