using ProtonPortSync.Infrastructure.Configuration;
using ProtonPortSync.Infrastructure.Qbittorrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ProtonPortSync.Application.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace ProtonPortSync.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddOptions<QbittorrentOptions>()
                .Bind(configuration.GetSection(QbittorrentOptions.SectionName))
                .ValidateOnStart();

            services
                .AddHttpClient<IQbittorrentPortService, QbittorrentPortService>(
                    (IServiceProvider ServiceProvider, HttpClient client) =>
                {
                    var options = ServiceProvider
                        .GetRequiredService<IOptions<QbittorrentOptions>>()
                        .Value;

                    client.BaseAddress = new Uri(options.BaseUrl);
                })
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    // Configure the primary HTTP message handler if needed
                });
                
            return services;
        }
    }
}
