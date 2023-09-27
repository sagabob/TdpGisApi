using Microsoft.Azure.Cosmos;
using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.DataProviders.Cosmos.Factory;

public class ComosClientFactory : IComosClientFactory
{
    public CosmosClient Create(ConnectionObject connectionObject)
    {
        return new CosmosClient(connectionObject.ConnectionParameters["URL"],
            connectionObject.ConnectionParameters["KEY"]);
        //TODO Will implement how to create CosmosClient with RBAC credentials
    }
}