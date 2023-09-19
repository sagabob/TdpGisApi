using TdpGisApi.Repository.CosmosDb.Repos;

namespace TdpGisApi.Repository.CosmosDb;

public interface ICosmosRepository
{
    Task<IOrderedQueryable<T>> Query<T>(string partitionKey);
    Task<List<T>> QuerySql<T>(string sql, object paramValues, string partitionKey) where T : class;
    Task<CosmosRepositoryResult<T>> Delete<T>(string id, string partitionKey) where T : class;
    Task<CosmosRepositoryResult<T>> Upsert<T>(string partitionKey, T model) where T : class;
}