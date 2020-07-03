using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TweeterBook.Data;
using TweeterBook.Extension;

namespace TweeterBook
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

                await dbContext.Database.MigrateAsync();

                var rolemanger = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
               
                UserAndRoleDataInitializer.SeedData(userManager, rolemanger);

                //if (await rolemanger.RoleExistsAsync("Admin"))
                //{
                //    var adminRole = new IdentityRole("Admin");
                //    await rolemanger.CreateAsync(adminRole);
                //}

                //if (await rolemanger.RoleExistsAsync("Employee"))
                //{
                //    var adminRole = new IdentityRole("Employee");
                //    await rolemanger.CreateAsync(adminRole);
                //}
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
