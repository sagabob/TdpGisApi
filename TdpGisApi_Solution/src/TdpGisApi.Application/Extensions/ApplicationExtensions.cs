using Microsoft.Extensions.DependencyInjection;
using TdpGisApi.Application.DataProviders.Cosmos;
using TdpGisApi.Application.DataProviders.Cosmos.Factory;
using TdpGisApi.Application.Handlers;
using TdpGisApi.Application.Handlers.Core;
using TdpGisApi.Application.Mapper;

namespace TdpGisApi.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingConfig));
        return services;
    }

    public static IServiceCollection RegisterHandlers(this IServiceCollection services)
    {
        services.AddScoped<IGisFeatureInfoHandler, GisFeatureInfoHandler>();
        return services;
    }

    public static IServiceCollection RegisterComosComponents(this IServiceCollection services)
    {
        services.AddSingleton<IComosClientFactory, ComosClientFactory>();
        services.AddSingleton<ICosmosRepositoryFactory, CosmosRepositoryFactory>();
        return services;
    }
}