using Newtonsoft.Json.Linq;
using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.DataProviders.Cosmos;

public interface ICosmosRepository
{
    Task<List<JObject>> QuerySql(string sql, QueryConfig featureConfig );
}