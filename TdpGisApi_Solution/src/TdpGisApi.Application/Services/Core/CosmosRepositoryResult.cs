using System.Net;

namespace TdpGisApi.Application.Services.Core;

public record CosmosRepositoryResult<T>(HttpStatusCode StatusCode, T Model, string ErrorMessage);