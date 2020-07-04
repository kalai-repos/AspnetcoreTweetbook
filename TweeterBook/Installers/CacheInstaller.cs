using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweeterBook.Caching;
using TweeterBook.Services;

namespace TweeterBook.Installers
{
    public class CacheInstaller : IInstallers
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var radiscachesetting = new RadisCacheSetting();
            configuration.GetSection(nameof(RadisCacheSetting)).Bind(radiscachesetting);
            services.AddSingleton(radiscachesetting);

            if (!radiscachesetting.Enabled)
            {
                return;
            }

            services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(radiscachesetting.ConnectionString));

            services.AddStackExchangeRedisCache(options => options.Configuration = radiscachesetting.ConnectionString);
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();

        }
    }
}
