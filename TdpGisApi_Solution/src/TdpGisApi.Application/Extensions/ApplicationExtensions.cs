using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TdpGisApi.Application.Context;
using TdpGisApi.Endpoints.Models;

namespace TdpGisApi.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection LoadApplicationConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var appSettings = new AppConfiguration();

            configuration.GetSection(nameof(AppConfiguration)).Bind(appSettings);

            services.AddDbContextFactory<CosmosGisAppContext>(optionsBuilder =>
                optionsBuilder
                    .UseCosmos(
                        appSettings.ConnectionString,
                        appSettings.Database,
                        options =>
                        {
                            options.ConnectionMode(ConnectionMode.Direct);
                            options.MaxRequestsPerTcpConnection(20);
                            options.MaxTcpConnectionsPerEndpoint(32);
                        }));

            return services;
        }
    }
}
