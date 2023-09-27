using TdpGisApi.Application.DataProviders.Cosmos.Repos;

namespace TdpGisApi.Application.DataProviders.Cosmos;

public interface ICosmosRepository
{
    Task<IOrderedQueryable<T>> Query<T>(string partitionKey);
    Task<List<T>> QuerySql<T>(string sql, IDictionary<string, string> parameters, string partitionKey) where T : class;
    Task<CosmosRepositoryResult<T>> Delete<T>(string id, string partitionKey) where T : class;
    Task<CosmosRepositoryResult<T>> Upsert<T>(string partitionKey, T model) where T : class;
}