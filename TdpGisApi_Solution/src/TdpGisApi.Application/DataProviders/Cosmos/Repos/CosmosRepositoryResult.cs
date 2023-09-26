using System.Net;

namespace TdpGisApi.Application.DataProviders.Cosmos.Repos;

public record CosmosRepositoryResult<T>(HttpStatusCode StatusCode, T Model, string ErrorMessage);