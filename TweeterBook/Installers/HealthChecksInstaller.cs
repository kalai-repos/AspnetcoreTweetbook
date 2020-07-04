using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweeterBook.Data;
using TweeterBook.HealthChecks;

namespace TweeterBook.Installers
{
    public class HealthChecksInstaller : IInstallers
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks().
                AddDbContextCheck<DataContext>().
                AddCheck<RedisHealthCheck>("Redis");

        }
    }
}
