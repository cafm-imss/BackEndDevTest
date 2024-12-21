using CAFM.Application.Contracts.Repos;
using CAFM.Persistence.Implementation.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CAFM.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration _config)
        {
            services.AddDbContext<CAFMDbContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("Default"),
                    opt => opt.MigrationsAssembly(typeof(CAFMDbContext).Assembly.FullName)));

            services.AddRepositories();

            return services;
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWorkOrderRepo, WorkOrderRepo>();
        }
    }
}
