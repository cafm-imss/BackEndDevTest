using CAFM.Core.Interfaces;
using CAFM.Core.Services;

namespace CAFM.API.Extension
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IWorkOrderService, WorkOrderService>();
            services.AddSignalR();
            return services;
        }
    }

}
