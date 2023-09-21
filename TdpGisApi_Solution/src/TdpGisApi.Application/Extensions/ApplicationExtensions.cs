using Microsoft.Extensions.DependencyInjection;
using TdpGisApi.Application.Mapper;

namespace TdpGisApi.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingConfig));
        return services;
    }
}