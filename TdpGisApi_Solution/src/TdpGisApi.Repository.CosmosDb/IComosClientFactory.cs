using Microsoft.Azure.Cosmos;
using TdpGisApi.Domain.Models;

namespace TdpGisApi.Application.Factory;

public interface IComosClientFactory
{
    CosmosClient Create(ConnectionObject connectionObject);
}