using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TweeterBook.Data;
using TweeterBook.Repository;

namespace TweeterBook.Installers
{
    public class DbInstaller : IInstallers
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<DataContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>() //options => options.SignIn.RequireConfirmedAccount = true
            .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();

            //string connectionString = configuration.GetConnectionString("DefaultConnection");
            //SetMigrate(connectionString);

           services.AddScoped<EmployeeRepository>();

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

        }

        private void SetMigrate(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(connectionString);
            using var context = new DataContext(optionsBuilder.Options);
            context.Database.Migrate();
        }
    }
}
