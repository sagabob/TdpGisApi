using System.Net;

namespace TdpGisApi.Repository.CosmosDb.Repos;

public record CosmosRepositoryResult<T>(HttpStatusCode StatusCode, T Model, string ErrorMessage);