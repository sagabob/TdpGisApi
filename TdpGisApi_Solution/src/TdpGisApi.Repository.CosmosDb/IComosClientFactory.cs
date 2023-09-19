using Microsoft.Azure.Cosmos;
using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.Factory;

public interface IComosClientFactory
{
    CosmosClient Create(ConnectionObject connectionObject);
}