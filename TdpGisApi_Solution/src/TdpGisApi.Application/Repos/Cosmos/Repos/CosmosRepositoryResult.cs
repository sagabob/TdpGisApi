using System.Net;

namespace TdpGisApi.Application.Repos.Cosmos.Repos;

public record CosmosRepositoryResult<T>(HttpStatusCode StatusCode, T Model, string ErrorMessage);