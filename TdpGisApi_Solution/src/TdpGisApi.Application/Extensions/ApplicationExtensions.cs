﻿using Microsoft.Extensions.DependencyInjection;
using TdpGisApi.Application.DataProviders.Cosmos;
using TdpGisApi.Application.DataProviders.Cosmos.Factory;
using TdpGisApi.Application.DataProviders.Cosmos.Helpers;
using TdpGisApi.Application.Handlers;
using TdpGisApi.Application.Handlers.Core;
using TdpGisApi.Application.Mappers;

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
        services.AddScoped<IGisFeatureDataHandler, GisFeatureDataHandler>();

        //Not sure whether we should make it singleton
        services.AddScoped<IGisFeatureDataCosmosHandler, GisFeatureDataCosmosHandler>();

        return services;
    }

    public static IServiceCollection RegisterComosComponents(this IServiceCollection services)
    {
        services.AddSingleton<ICosmosQueryHelpers, CosmosQueryHelpers>();
        services.AddSingleton<IComosClientFactory, ComosClientFactory>();
        services.AddSingleton<ICosmosRepositoryFactory, CosmosRepositoryFactory>();
        return services;
    }
}