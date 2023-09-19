using Microsoft.Azure.Cosmos;
using TdpGisApi.Application.Models;

namespace TdpGisApi.Repository.CosmosDb;

public interface IComosClientFactory
{
    CosmosClient Create(ConnectionObject connectionObject);
}