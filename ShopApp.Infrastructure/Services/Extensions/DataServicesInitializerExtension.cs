using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopApp.Infrastructure.Data;

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
    }
}
