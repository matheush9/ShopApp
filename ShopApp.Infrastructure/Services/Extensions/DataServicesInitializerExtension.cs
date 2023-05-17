using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopApp.Infrastructure.Data;
using System;

namespace ShopApp.Infrastructure.Services.Extensions
{
    public static partial class DataServicesInitializerExtension
    {
        public static IServiceCollection RegisterDataServices(
          this IServiceCollection services, IConfiguration configuration)
        {
            RegisterDataContext(services, configuration);
            return services;
        }

        public static void RegisterDataContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("ShopConnectionString")));
        }

        public static void ApplyMigration(this IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.CreateScope();
            var serviceDb = serviceScope.ServiceProvider.GetService<DataContext>();

            serviceDb?.Database.Migrate();            
        }
    }
}
