using TdpGisApi.Application.Models;

namespace TdpGisApi.Application.Handlers;

public interface ILoadAppConfigurationHandler
{
    Task<List<ConnectionObject>> Connections();

    Task<List<QueryConfig>> Features();
}