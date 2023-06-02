using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        public static IHost MigrateDatabase(this IHost host)
        {
            bool success = false;
            do
            {
                using (var scope = host.Services.CreateScope())
                {
                    using (var appContext = scope.ServiceProvider.GetRequiredService<DataContext>())
                    {
                        try
                        {
                            appContext.Database.Migrate();
                            success = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Migration failed: {ex.Message}");
                        }
                    }
                }

                if (!success)
                {
                    Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                }
            } while (!success);

            return host;
        }
    }
}