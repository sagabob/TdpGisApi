using Microsoft.Azure.Cosmos;
using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.DataProviders.Cosmos;

public interface IComosClientFactory
{
    CosmosClient Create(ConnectionObject connectionObject);
}