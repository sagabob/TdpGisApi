using Microsoft.Azure.Cosmos;
using TdpGisApi.Configuration.Models.Core;

namespace TdpGisApi.Application.Factory;

public interface IComosClientFactory
{
    CosmosClient Create(ConnectionObject connectionObject);
}